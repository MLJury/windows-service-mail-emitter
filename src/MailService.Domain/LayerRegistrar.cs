using System;
using AppCore.IOC;
using DMN = MailService.Domain;
using ioc = AppCore.IOC;
using Config = System.Configuration.ConfigurationManager;
using MailService.Core.Service;
using System.Timers;

[assembly: AppCore.IOC.Registrar(typeof(MailService.Domain.LayerRegistrar), Order = 1)]
namespace MailService.Domain
{
    using ASM = System.Reflection.Assembly;
    using svc = Core.Service;
    class LayerRegistrar : IRegistrar
    {
        readonly Guid _layerID = Guid.NewGuid();

        public Guid ID => _layerID;

        public void Start(IContainer container)
        {
            ASM asmInterfaces = ASM.GetAssembly(typeof(svc.IService)),
                asmClasses = ASM.GetAssembly(this.GetType());

            container.RegisterFromAssembly(
                servicesAssembly: asmInterfaces,
                implementationsAssembly: asmClasses,
                isService: t => t.IsInterface && !t.IsClass && typeof(svc.IService).IsAssignableFrom(t),
                isServiceImplementation: t => !t.IsInterface && t.IsClass && t.IsSubclassOf(typeof(DMN.Service))
                );
        }
    }
}
