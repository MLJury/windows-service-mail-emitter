using MailService.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MailService.Core.Model;

namespace MailService.Core.Service
{
    public interface IQueueService : IService
    {
        AppCore.Result Enqueue(IEnumerable<Mail> recMsg);

        AppCore.Result Enqueue(Mail recMsg);

        AppCore.Result Enqueue(Library.Queue.Packet<QueueItem> queuePacket);
        Library.Queue.ITransaction<QueueItem> Dequeue(Library.Queue.Priority priority, TimeSpan timeSpan);

        int QueueCount(Library.Queue.Priority priority);

        int QueueCount();

    }
}
