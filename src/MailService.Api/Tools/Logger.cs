namespace MailService.Tools
{
    class Logger: AppCore.EventLogger.WindowsEventLogger, Core.IEventLogger
    {
        public Logger()
            : base("MailService")
        {
        }
    }
}
