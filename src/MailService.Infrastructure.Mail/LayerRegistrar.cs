using System;
using AppCore.IOC;
using MAGFA = MailService.Infrastructure.Mail;
using MailService.Core.Mail;

[assembly: Registrar(typeof(MAGFA.LayerRegistrar))]
namespace MailService.Infrastructure.Mail
{
    using ASM = System.Reflection.Assembly;
    using svc = Core.Mail;

    class LayerRegistrar : IRegistrar
    {
        readonly Guid _layerID = Guid.NewGuid();

        public Guid ID => _layerID;

        public void Start(IContainer container)
        {
            ASM asmInterfaces = ASM.GetAssembly(typeof(svc.IMagfaService)),
                asmClasses = ASM.GetAssembly(this.GetType());

            container.RegisterFromAssembly(
                servicesAssembly: asmInterfaces,
                implementationsAssembly: asmClasses,
                isService: t => t.IsInterface && !t.IsClass && typeof(svc.IMagfaService).IsAssignableFrom(t),
                isServiceImplementation: t => !t.IsInterface && t.IsClass && t.IsSubclassOf(typeof(MAGFA.Service))
                );
        }
    }
}
