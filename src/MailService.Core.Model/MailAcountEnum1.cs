using System;
using System.Collections.Concurrent;

namespace MailService.Core.Model
{
		public enum MailServiceAccounts : byte
		{
				Unknown = 0,
			}
	    public class MailServiceDic
		{
		readonly static Lazy<MailServiceDic> _instance = new Lazy<MailServiceDic>(() => new MailServiceDic());

		        public static MailServiceDic Instance
             => _instance.Value;

				readonly ConcurrentDictionary<MailServiceAccounts, Guid> _items = new ConcurrentDictionary<MailServiceAccounts, Guid>()
		{
			[MailServiceAccounts.Unknown] = Guid.Empty,
		};

		public Guid this[MailServiceAccounts account]
        {
            get
            {
                Guid accountId = Guid.Empty;
                _items.TryGetValue(account, out accountId);
                return accountId;
            }
        }
		}
}
