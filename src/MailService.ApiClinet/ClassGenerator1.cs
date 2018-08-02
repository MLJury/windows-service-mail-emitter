// App: MailService.Client
// Developer: Payam Kandi
// Version: 1.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MailService.ApiClient.Interface;
using model = MailService.Core.Model;

namespace MailService.ApiClient
{
	/// <summary>
    /// flag for ioc container registrar
    /// </summary>
	abstract class Service
    {
    }

	partial class MailService: Service, IMailService
	{
		public MailService(IMailServiceClient client)
		{
			_client = client;
		}
		readonly IMailServiceClient _client;

        
		public virtual Task<AppCore.Result<model.Mail>> Send(model.Mail msg,IDictionary<string, string> httpHeaders = null)
		{
			var routeParamValues = new Dictionary<string, string>{{"msg",msg.ToString()}};
			const string url = "api/v1/mail/Send";
			return _client.SendAsync<model.Mail>(true, url, routeParamValues, httpHeaders, msg);
		}
        
		public virtual Task<AppCore.Result> Send(model.Mail[] msg,IDictionary<string, string> httpHeaders = null)
		{
			var routeParamValues = new Dictionary<string, string>{{"msg",msg.ToString()}};
			const string url = "api/v1/mail/Send/Bulk";
			return _client.SendAsync(true, url, routeParamValues, httpHeaders, msg);
		}
	}

	partial class SendService: Service, ISendService
	{
		public SendService(IMailServiceClient client)
		{
			_client = client;
		}
		readonly IMailServiceClient _client;

        
		public virtual Task<AppCore.Result> Pause(IDictionary<string, string> httpHeaders = null)
		{
			var routeParamValues = new Dictionary<string, string>{};
			const string url = "api/v1/Send/Pause";
			return _client.SendAsync(true, url, routeParamValues, httpHeaders);
		}
        
		public virtual Task<AppCore.Result> Resume(IDictionary<string, string> httpHeaders = null)
		{
			var routeParamValues = new Dictionary<string, string>{};
			const string url = "api/v1/Send/Resume";
			return _client.SendAsync(true, url, routeParamValues, httpHeaders);
		}
	}

	partial class SendTryService: Service, ISendTryService
	{
		public SendTryService(IMailServiceClient client)
		{
			_client = client;
		}
		readonly IMailServiceClient _client;

	}
}