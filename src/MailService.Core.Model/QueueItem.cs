using System;

namespace MailService.Core.Model
{
    public class QueueItem
    {
        public Guid SourceAccountID { get; set; }

        public Core.Model.Mail Mail { get; set; }
    }
}
