using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ds = MailService.Core.DataSource;
using svc = MailService.Core.Service;
using @Model = MailService.Core.Model;
using Config = System.Configuration.ConfigurationManager;
using System.Timers;

namespace MailService.Domain
{
    class MailService : Service, svc.IMailService
    {
        public MailService(AppCore.IOC.IContainer container
                              , ds.IMailDataSource mailDataSource)
            : base(container)
        {
            _mailDataSource = mailDataSource;
        }

        readonly ds.IMailDataSource _mailDataSource;

        public Task<AppCore.Result<Core.Model.Mail>> SendAsync(Model.Mail msg)
        {
            throw new Exception();
            msg.ID = Guid.NewGuid();
            msg.Priority = msg.Priority == Model.Priority.Unknown ? Model.Priority.Medium : msg.Priority;
            return _mailDataSource.CreateAsync(msg);
        }

        public Task<AppCore.Result> SendAsync(Model.Mail[] messages)
        {
            messages.ToList().ForEach(f => f.ID = Guid.NewGuid());
            messages.Where(w => w.Priority == Model.Priority.Unknown).ToList().ForEach(f => f.Priority = Model.Priority.Medium);
            return _mailDataSource.CreateAsync(messages);
        }
    }
}
