using System.Threading.Tasks;

namespace MailService.Core.Mail
{
    public interface IOutgoingService: IMagfaService
    {
        //Model.Account account, Model.Message message
        Task<AppCore.Result> SendAsync(Model.Mail mail, Model.Account account);
    }
}
