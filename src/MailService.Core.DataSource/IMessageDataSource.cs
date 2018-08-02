using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailService.Core.DataSource
{
	public interface IMailDataSource : IDataSource
	{
        Task<AppCore.Result<Model.Mail>> CreateAsync(Model.Mail model);

        Task<AppCore.Result> CreateAsync(Model.Mail[] model);

        Task<AppCore.Result> DeleteAsync(IEnumerable<Guid> ids);

        Task<AppCore.Result<IEnumerable<Model.Mail>>> ListAsync(Guid sourceAccountID, Core.Model.Status? status = null, string receiverNumber = null, DateTime? sendDateFrom = null, DateTime? sendDateTo = null);

        Task<AppCore.Result<IEnumerable<Model.Mail>>> ListUnQueueAsync(Guid sourceAccountID);

        Task<AppCore.Result<IEnumerable<Model.MailReceiverDetail>>> ListReceiverAsync(Guid Id);

        Task<AppCore.Result> SetQueueAsync(Guid receiverId, bool isQueue);

        Task<AppCore.Result> SetQueueAsync(List<Model.Mail> mails, bool isQueue);

        Task<AppCore.Result> SetSendAsync(Model.Send send);
    }
}
