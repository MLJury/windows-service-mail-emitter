using System;
using System.Collections.Generic;

namespace MailService.Core.Model
{
    public class Send : Model
    {
        public Guid MailID { get; set; }
        public bool IsSent { get; set; }
        public string Message { get; set; }
    }
}
