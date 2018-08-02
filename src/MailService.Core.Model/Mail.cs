using System;
using System.Collections.Generic;

namespace MailService.Core.Model
{
    public class Mail: Model
    {
        public MailServiceAccounts SourceAccount { get; set; }

        public Guid SourceAccountID { get; set; }

        public string SourceAccountTitle { get; set; }

        public SendType SendType { get; set; }

        public Status Status { get; set; }

        public Priority Priority { get; set; }

        public string Content { get; set; }

        public string Subject { get; set; }

        public string SourceEmail { get; set; }

        public Boolean IsSent { get; set; }

        public DateTime? SendDate { get; set; }

        public Boolean IsQueue { get; set; }

        public DateTime? QueueDate { get; set; }

        public List<MailReceiverDetail> EmailReceivers { get; set; }
    }
}
