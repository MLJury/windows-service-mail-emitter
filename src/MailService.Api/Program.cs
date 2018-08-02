using System;
using System.Linq;
using System.ServiceProcess;
using MailService.Core.Model;
using Microsoft.Owin.Hosting;
using MailService.Api;
using System.Security.Permissions;

namespace MailService
{
    class Program
    {
        const string title = "_________ Kama MailService _______________";
        static void Main(string[] args)
        {
            try
            {
                var service = new Service();
                if (Environment.UserInteractive)
                {
                    Console.WriteLine(title);
                    Console.WriteLine("Starting service, Please wait...");
                    service.Start(args);
                    Console.ReadKey();
                    service.Stop();
                }
                else
                    ServiceBase.Run(service);
            }
            catch (Exception e)
            {
                Console.Write($"-----------------------\n {e.Message}");
                Console.ReadKey();
            }
        }
    }
}

