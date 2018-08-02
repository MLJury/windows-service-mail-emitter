using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ds = MailService.Core.DataSource;
using @Model = MailService.Core.Model;

namespace MailService.Infrastructure.DAL
{
	class MailDataSource : DataSource, ds.IMailDataSource
	{
		public MailDataSource(AppCore.IOC.IContainer container)
			: base(container)
		{
		}

        public async Task<AppCore.Result<Model.Mail>> CreateAsync(Model.Mail model)
        {
            try
            {
                var result = (await _dbMail.AddMailAsync(
                    _id: model.ID,
                    _sourceAccountID: Model.MailServiceDic.Instance[model.SourceAccount],
                    _priority: (byte)model.Priority,
                    _sendType: (byte)model.SendType,
                    _status: (short)model.Status,
                    _content: model.Content,
                    _subject: model.Subject,
                    _encodingType: null,
                    _mailReveivers: Newtonsoft.Json.JsonConvert.SerializeObject(model.EmailReceivers)
                    )).ToActionResult<Model.Mail>();

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AppCore.Result> CreateAsync(Model.Mail[] model)
        {
            try
            {
                var result = (await _dbMail.AddMailsAsync(
                        _messages: Newtonsoft.Json.JsonConvert.SerializeObject(model)
                    )).ToActionResult();

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AppCore.Result> DeleteAsync(IEnumerable<Guid> ids)
        {
            try
            {
                var result = (await _dbMail.DeleteMailAsync(_iDS: $"[{string.Join(",", ids.ToArray())}]"))
                        .ToActionResult();

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AppCore.Result<IEnumerable<Model.Mail>>> ListAsync(Guid sourceAccountID, Core.Model.Status? status = null, string receiverNumber = null, DateTime? sendDateFrom = null, DateTime? sendDateTo = null)
        {
            try
            {
                short? xStatus = null;
                if (status != null && status.HasValue)
                    xStatus = (short)status;

                var result = (await _dbMail.GetMailsAsync()).ToListActionResult<Model.Mail>();

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AppCore.Result<IEnumerable<Model.Mail>>> ListUnQueueAsync(Guid sourceAccountID)
        {
            try
            {
                var result = (await _dbMail.GetUnQueueMailsAsync())
                        .ToListActionResult<Model.Mail>();

                foreach(var item in result.Data)
                    item.EmailReceivers = (await ListReceiverAsync(item.ID)).Data.ToList();

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AppCore.Result<IEnumerable<Model.MailReceiverDetail>>> ListReceiverAsync(Guid Id)
        {
            try
            {
                var result = (await _dbMail.GetMailReceiversAsync(_id: Id))
                        .ToListActionResult<Model.MailReceiverDetail>();

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AppCore.Result> SetQueueAsync(Guid Id, bool isQueue)
        {
            try
            {
                var result = (await _dbMail.SetQueueMailAsync(_id: Id, _isQueue: isQueue))
                        .ToListActionResult<AppCore.Result>();

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AppCore.Result> SetQueueAsync(List<Model.Mail> mails, bool isQueue)
        {
            try
            {
                var result = (await _dbMail.SetQueueMailsAsync(_iDs: Newtonsoft.Json.JsonConvert.SerializeObject(mails.Select(s => s.ID)), _isQueue: isQueue))
                        .ToListActionResult<AppCore.Result>();

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AppCore.Result> SetSendAsync(Model.Send send)
        {
            send.ID = Guid.NewGuid();
            try
            {
                var result = (await _dbMail.SetSendMailAsync(
                                _id: send.ID
                                , _mailID: send.MailID
                                , _isSent: send.IsSent
                                , _message: send.Message)).ToListActionResult<AppCore.Result>();

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
