using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Builder;
using Microsoft.Owin.Host;
using System.Web.Http;
using Asm = System.Reflection.Assembly;
using Config = System.Configuration.ConfigurationManager;
using System.Linq;
using System.Threading;
using System.Web.Http.ExceptionHandling;
using Mefa.Azmoon.API.Auth.Filters;
using API.Exceptions.Filters;
using MailService.Core.Service;
using System.Timers;

[assembly: OwinStartup(typeof(MailService.Api.Startup))]
[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(MailService.Api.Startup), "Started")]

namespace MailService.Api
{
    public class Startup
    {
        public string Host { get; private set; }

        private HttpConfiguration EnableWebApi()
        {
            var config = new HttpConfiguration();
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{vesion}/{controller}/{id}",
                new { vesion = "v1", id = RouteParameter.Optional });

            return config;
        }

        public void Configuration(IAppBuilder app)
        {
            var httpConfiguarion = EnableWebApi();


            IOC.Activator.Instance.ActiveWebApi(httpConfiguarion, new Asm[] { Asm.GetExecutingAssembly() });
            var apiPath = System.IO.Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName;
            AppCore.IOC.Loader.Load(IOC.Activator.Container, System.IO.Path.Combine(apiPath, @"Libs"));
            RegisterFilters(IOC.Activator.Container, httpConfiguarion);
            RegisterTimer(IOC.Activator.Container);
            app.UseWebApi(httpConfiguarion);
        }

        private static void RegisterFilters(AppCore.IOC.IContainer container, HttpConfiguration httpConfig)
        {
            //regsiter Filters
            container.RegisterType(typeof(ValidationFilter));
            container.RegisterType(typeof(KamaExceptionLogger));

            //register command filters in httConfig
            httpConfig.Filters.Add(container.Resolve<ValidationFilter>());
            httpConfig.Services.Replace(typeof(IExceptionLogger), container.Resolve<KamaExceptionLogger>());
        }

        private void RegisterTimer(AppCore.IOC.IContainer container)
        {
            System.Timers.Timer _timer;
            _timer = new System.Timers.Timer(Int32.Parse(Config.AppSettings["AutomationTimerInterval"]) * 1000);
            _timer.Elapsed += _GetTimerServices(container);
            if (Config.AppSettings["ServerType"] == "Internet")
                _timer.Start();
        }

        private ElapsedEventHandler _GetTimerServices(AppCore.IOC.IContainer container)
        {
            ElapsedEventHandler triggerMethod = container.Resolve<ISendService>().DoOnMainTimer;
            return triggerMethod;
        }
    }
}
