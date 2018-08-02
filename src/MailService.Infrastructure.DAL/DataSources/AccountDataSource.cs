using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ds = MailService.Core.DataSource;
using @Model = MailService.Core.Model;

namespace MailService.Infrastructure.DAL
{
	class AccountDataSource : DataSource, ds.IAccountDataSource
	{
		public AccountDataSource(AppCore.IOC.IContainer container)
			: base(container)
		{
		}

        private async Task<AppCore.Result> ModifyAsync(bool isNewRecord, Model.Account model)
        {
            try
            {
                return (await _dbPublic.ModifyAccountAsync(
                                _isNewRecord: isNewRecord
                                , _id: model.ID
                                , _title: model.Title
                                , _email: model.Email
                                , _password: model.Password
                                , _host: model.Host
                                , _port: model.Port
                                , _sSL: model.SSL
                                , _enabled: model.Enabled))
                        .ToActionResult();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<AppCore.Result> CreateAsync(Model.Account model)
            => ModifyAsync(true, model);

        public Task<AppCore.Result> UpdateAsync(Model.Account model)
            => ModifyAsync(false, model);

        public async Task<AppCore.Result> DeleteAsync(Guid id)
        {
            try
            {
                return (await _dbPublic.DeleteAccountAsync(_id: id))
                       .ToActionResult();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<AppCore.Result<IEnumerable<Model.Account>>> listAsync(bool? enabled)
        {
            try
            {
                return (await _dbPublic.GetAccountsAsync(_enabled: enabled))
                        .ToListActionResult<Model.Account>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<AppCore.Result<IEnumerable<Model.Account>>> ListAsync()
            => listAsync(null);

        public Task<AppCore.Result<IEnumerable<Model.Account>>> ActivesAsync()
            => listAsync(true);

        public Task<AppCore.Result<IEnumerable<Model.Account>>> InactivesAsync()
            => listAsync(false);

        public async Task<AppCore.Result<Model.Account>> GetAsync(Guid id)
        {
            try
            {
                var result = (await _dbPublic.GetAccountAsync(_id: id))
                        .ToActionResult<Model.Account>();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
;