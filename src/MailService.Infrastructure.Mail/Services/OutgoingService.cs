using System.Threading.Tasks;
using System.Web;
using System.Net.Mail;
using @Model = MailService.Core.Model;
using System.ComponentModel;
using System.Net;
using System.Linq;
using System;

namespace MailService.Infrastructure.Mail
{
    class OutgoingService : Service, Core.Mail.IOutgoingService
    {
        public async Task<AppCore.Result> SendAsync(Model.Mail mail, Model.Account account)
        {
            try
            {
                var result = await SendCoreAsync(
                    mail: mail
                    , account: account);

                return AppCore.Result.Successful();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
