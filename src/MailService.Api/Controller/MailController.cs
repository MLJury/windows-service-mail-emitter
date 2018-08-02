using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Reflection;

namespace MailService.Api.Controllers
{
    [RoutePrefix("api/v1/mail")]
    public class MailController : BaseApiController
    {
        public MailController(Core.Service.IMailService mailService)
        {
            _mailService = mailService;
        }

        readonly Core.Service.IMailService _mailService;

        [HttpPost, Route("Send")]
        public Task<AppCore.Result<Core.Model.Mail>> Send(Core.Model.Mail msg)
            => _mailService.SendAsync(msg);


        [HttpPost, Route("Send/Bulk")]
        public Task<AppCore.Result> Send(Core.Model.Mail[] msg)
            => _mailService.SendAsync(msg);
    }
}
