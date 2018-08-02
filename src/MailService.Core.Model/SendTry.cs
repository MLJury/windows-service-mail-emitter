using System;
using System.Collections.Generic;

namespace MailService.Core.Model
{
    public class SendTry : Model
    {
        public Guid MailID { get; set; }
        public DateTime Date { get; set; }
        public bool Succeed { get; set; }
        public string Message { get; set; }
    }
}
