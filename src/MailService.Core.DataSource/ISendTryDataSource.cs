using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailService.Core.DataSource
{
    public interface ISendTryDataSource : IDataSource
    {
        Task<AppCore.Result> CreateTrySendAsync(Model.SendTry sendTry);
    }
}
