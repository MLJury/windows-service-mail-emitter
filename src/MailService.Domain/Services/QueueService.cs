using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ds = MailService.Core.DataSource;
using svc = MailService.Core.Service;
using @Model = MailService.Core.Model;
using MailService.Core.Model;
using System.Linq;
using @model = MailService.Core.Model;

namespace MailService.Domain
{
    class QueueService : Service, svc.IQueueService
    {
        public QueueService(AppCore.IOC.IContainer container)
            : base(container)
        {
            _queue = new Library.Queue.Queue<QueueItem>("MailService", new QueueItemSerializer(_objectSerializer));
        }
        readonly Library.Queue.Queue<QueueItem> _queue;

        private Library.Queue.Packet<QueueItem> ConvertToPacket(Core.Model.Mail mail)
              => new Library.Queue.Packet<QueueItem>
              {
                  Label = mail.ID.ToString("N"),
                  AppSpecific = 1,
                  CorrelationId = mail.ID.ToString("N"),
                  Id = mail.ID,
                  Priority = (Library.Queue.Priority)((byte)mail.Priority),
                  Data = new QueueItem { SourceAccountID = mail.SourceAccountID, Mail = mail },
                  TryCount = 0
              };

        public AppCore.Result Enqueue(IEnumerable<Core.Model.Mail> mail)
        {
            _queue.Enqueue(mail.Select(s => ConvertToPacket(s)));
            return AppCore.Result.Successful();
        }

        public AppCore.Result Enqueue(Core.Model.Mail mail)
        {
            _queue.Enqueue(ConvertToPacket(mail));
            return AppCore.Result.Successful();
        }

        public AppCore.Result Enqueue(Library.Queue.Packet<Model.QueueItem> queuePacket)
        {
            _queue.Enqueue(queuePacket);
            return AppCore.Result.Successful();
        }

        public Library.Queue.ITransaction<model.QueueItem> Dequeue(Library.Queue.Priority priority, TimeSpan timeSpan)
        {
            var qResult = _queue.Dequeue(priority, timeSpan);
            qResult.Data.TryCount = qResult.Data.TryCount + 1;
            qResult?.Commit();
            return qResult;
        }

        public int QueueCount()
            => (QueueCount(Library.Queue.Priority.VeryHeigh)
                + QueueCount(Library.Queue.Priority.High)
                + QueueCount(Library.Queue.Priority.Medium)
                + QueueCount(Library.Queue.Priority.Normal));

        public int QueueCount(Library.Queue.Priority priority)
            => _queue[priority].Count();
    }
}
