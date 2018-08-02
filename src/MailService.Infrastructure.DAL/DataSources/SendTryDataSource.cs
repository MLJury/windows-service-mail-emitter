using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ds = MailService.Core.DataSource;
using @Model = MailService.Core.Model;

namespace MailService.Infrastructure.DAL
{
    class SendTryDataSource : DataSource, ds.ISendTryDataSource
    {
        public SendTryDataSource(AppCore.IOC.IContainer container)
            : base(container)
        {
        }

        public async Task<AppCore.Result> CreateTrySendAsync(Model.SendTry sendTry)
        {
            try
            {
                return (await _dbMail.AddTrySendMailAsync(
                    _mailID: sendTry.MailID
                    , _message: sendTry.Message
                    , _succeed: sendTry.Succeed)).ToActionResult();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
