using System;
using AppCore.IOC;

[assembly: Registrar(typeof(MailService.ApiClient.LayerRegistrar))]
namespace MailService.ApiClient
{
    class LayerRegistrar : IRegistrar
    {
        readonly Guid _id = Guid.NewGuid();
        public Guid ID => _id;

        public void Start(IContainer container)
        {
            var clientAssembly = System.Reflection.Assembly.GetAssembly(this.GetType());

            container.RegisterFromAssembly(
                servicesAssembly: clientAssembly,
                implementationsAssembly: clientAssembly,
                isService: t => t.IsInterface && t != typeof(Interface.IService) && typeof(Interface.IService).IsAssignableFrom(t),
                isServiceImplementation: t => !t.IsInterface && t.IsClass && t.IsSubclassOf(typeof(Service))
                );

            var hostInfo = container.TryResolve<Interface.IMailServiceHostInfo>();
            var objectSerializer = container.TryResolve<AppCore.IObjectSerializer>();

            container.RegisterInstance<Interface.IMailServiceClient>(new MailServiceClient(objectSerializer, hostInfo.Host, hostInfo.GetDefaultHeaders));
        }
    }
}
