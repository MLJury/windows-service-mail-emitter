using System;
using System.Collections.Generic;

namespace MailService.Core.Model
{
    public class MailReceiverDetail
    {
        public Guid ID { get; set; }
        public string Email { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
    }
}
