// App: MailService.Client
// Developer: Payam Kandi
// Version: 1.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model = MailService.Core.Model;


namespace MailService.ApiClient.Interface
{
	public interface IMailServiceClient: Library.ApiClient.IClient
	{
		string Host{get;}
	}

	public interface IMailServiceHostInfo: Library.ApiClient.IHostInfo
	{
	}

	/// <summary>
    /// flag for ioc container registrar
    /// </summary>
	public interface IService
	{
	}

	public interface IMailService: IService
	{
		
		Task<AppCore.Result<model.Mail>> Send(model.Mail msg,IDictionary<string, string> httpHeaders = null);
		
		Task<AppCore.Result> Send(model.Mail[] msg,IDictionary<string, string> httpHeaders = null);
	}
	public interface ISendService: IService
	{
		
		Task<AppCore.Result> Pause(IDictionary<string, string> httpHeaders = null);
		
		Task<AppCore.Result> Resume(IDictionary<string, string> httpHeaders = null);
	}
	public interface ISendTryService: IService
	{
	}
}