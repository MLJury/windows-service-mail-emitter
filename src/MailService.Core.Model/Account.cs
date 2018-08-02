
namespace MailService.Core.Model
{
    public class Account: Model
    {
        public string Title { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool Enabled { get; set; }

        public bool SSL { get; set; }

        public int Port { get; set; }

        public string Host { get; set; }

        public override string ToString()
            => Title;
    }
}
