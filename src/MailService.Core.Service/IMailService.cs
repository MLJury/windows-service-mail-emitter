using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace MailService.Core.Service
{
	public interface IMailService : IService
	{
        Task<AppCore.Result<Core.Model.Mail>> SendAsync(Model.Mail msg);
        Task<AppCore.Result> SendAsync(Model.Mail[] model);
    }
}
