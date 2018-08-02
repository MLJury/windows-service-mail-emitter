using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using svc = MailService.Core.Service;
using @Model = MailService.Core.Model;
using Config = System.Configuration.ConfigurationManager;
using System.Threading;
using System.Timers;
using S22.Imap;
//using AE.Net.Mail;

namespace MailService.Domain
{
    class SendService : Service, svc.ISendService
    {
        public SendService(AppCore.IOC.IContainer container
                           , Core.IEventLogger logger
                           , Core.DataSource.IAccountDataSource accountDataSource
                           , Core.DataSource.IConfigDataSource configDataSource
                           , Core.DataSource.IMailDataSource messageDataSource
                           , Core.DataSource.ISendTryDataSource sendTryDataSource
                           , Core.Mail.IOutgoingService mailOutgoingService
                           , Core.Service.IQueueService queueService)
            : base(container)
        {
            _logger = logger;
            _queueService = queueService;
            _accountDataSource = accountDataSource;
            _configDataSource = configDataSource;
            _messageDataSource = messageDataSource;
            _mailOutgoingService = mailOutgoingService;
            _sendTryDataSource = sendTryDataSource;
        }
        readonly Core.IEventLogger _logger;
        readonly Core.Service.IQueueService _queueService;
        readonly Core.DataSource.IAccountDataSource _accountDataSource;
        readonly Core.DataSource.IConfigDataSource _configDataSource;
        readonly Core.DataSource.IMailDataSource _messageDataSource;
        readonly Core.Mail.IOutgoingService _mailOutgoingService;
        readonly Core.DataSource.ISendTryDataSource _sendTryDataSource;

        public AppCore.Result ResumeProcess()
        {
            QueueHelper.Instance.Running = true;
            return AppCore.Result.Successful();
        }

        public AppCore.Result PauseProcess()
        {
            QueueHelper.Instance.Running = false;
            return AppCore.Result.Successful();
        }

        private async Task _SendFailedAsync(Library.Queue.ITransaction<Model.QueueItem> qResult, string message)
        {
            if (Int32.Parse(Config.AppSettings["MaximumSendTry"]) > qResult.Data.TryCount)
            {
                await _sendTryDataSource.CreateTrySendAsync(new Model.SendTry
                {
                    MailID = qResult.Data.Id
                    ,
                    Message = message
                    ,
                    Succeed = false
                });
                _queueService.Enqueue(qResult.Data);
            }
            else
                await _messageDataSource.SetSendAsync(new Model.Send
                {
                    MailID = qResult.Data.Id
                    ,
                    IsSent = false
                    ,
                    Message = message
                });
        }

        private async Task<AppCore.Result> _SendAsync(Model.Mail mail)
        {
            var account = await _accountDataSource.GetAsync(mail.SourceAccountID);
            if (!account.Success)
                return AppCore.Result.Failure(message: account.Message, code: account.Code);

            return await _mailOutgoingService.SendAsync(mail, account.Data);
        }


        public async Task<AppCore.Result> LoadAsync()
        {
            var rsGetConfig = await _configDataSource.GetPrioritySendCountAsync();
            if (!rsGetConfig.Success)
                return AppCore.Result.Failure(message: rsGetConfig.Message);
            else if (rsGetConfig.Data == null)
                return AppCore.Result.Failure(message: "Service configuration not exist.");

            QueueHelper.Instance.Load(rsGetConfig.Data);

            return AppCore.Result.Successful();
        }

        public async Task<AppCore.Result> ProcessAsync()
        {
            if (_queueService.QueueCount(QueueHelper.Instance.CurrentPriority).Equals(0))
                QueueHelper.Instance.Next(_queueService);

            while (QueueHelper.Instance.Running)
            {
                Library.Queue.ITransaction<Model.QueueItem> qResult = null;
                try
                {
                    qResult = _queueService.Dequeue(QueueHelper.Instance.CurrentPriority, TimeSpan.FromMilliseconds(300));

                    var sendResult = await _SendAsync(qResult.Data.Data.Mail);

                    if (!sendResult.Success)
                        await _SendFailedAsync(qResult, sendResult.Message);
                    else
                        await _messageDataSource.SetSendAsync(new Model.Send
                        {
                            MailID = qResult.Data.Id
                            , 
                            IsSent = true
                            , 
                            Message = "Message was sent successfully"
                        });
                }
                catch (Exception e)
                {
                    _logger?.Error(e);
                    await _SendFailedAsync(qResult, e.Message);
                }
                finally
                {
                    QueueHelper.Instance.Next(_queueService);
                }
            }

            return AppCore.Result.Successful();
        }
        public void DoOnMainTimer(object sender, ElapsedEventArgs e)
        {
            var unQueueList = _messageDataSource.ListUnQueueAsync(Guid.Empty).GetAwaiter().GetResult();

            if (unQueueList.Data.Any())
            {
                var setQueueResult = _messageDataSource.SetQueueAsync(unQueueList.Data.ToList(), true).GetAwaiter().GetResult();
                if (setQueueResult.Success)
                {
                    _queueService.Enqueue(unQueueList.Data);
                    QueueHelper.Instance.Running = true;
                    ProcessAsync().GetAwaiter().GetResult();
                }
            }
        }
    }
}
