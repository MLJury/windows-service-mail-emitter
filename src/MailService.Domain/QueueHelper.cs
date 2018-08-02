using System;
using System.Linq;

namespace MailService.Domain
{
    class QueueHelper
    {
        readonly static Lazy<QueueHelper> _instance = new Lazy<QueueHelper>(() => new QueueHelper());

        public static QueueHelper Instance
            => _instance.Value;

        Core.Model.PrioritySendCount config = null;
        Core.Model.Priority priority = Core.Model.Priority.VeryHigh;
        ushort counter = 0;

        public bool Running { get; set; }

        public void Load(Core.Model.PrioritySendCount config)
        {
            this.config = config;
            this.Running = true;
        }

        public Library.Queue.Priority CurrentPriority
              => (Library.Queue.Priority)((byte)priority);

        public void Next(Core.Service.IQueueService queueService)
        {
            if (queueService.QueueCount() > 0)
            {
                Next();
                while (queueService.QueueCount((Library.Queue.Priority)priority) == 0)
                    Next();
            }
            else
                Running = false;
        }

        private void Next()
        {
            counter++;

            if (priority == Core.Model.Priority.VeryHigh && counter > config.VeryHigh)
            {
                counter = 0;
                priority = Core.Model.Priority.High;
            }
            else if (priority == Core.Model.Priority.High && counter > config.High)
            {
                counter = 0;
                priority = Core.Model.Priority.Medium;
            }
            else if (priority == Core.Model.Priority.Medium && counter > config.Medium)
            {
                counter = 0;
                priority = Core.Model.Priority.Normal;
            }
            else if (priority == Core.Model.Priority.Normal && counter > config.Normal)
            {
                counter = 0;
                priority = Core.Model.Priority.VeryHigh;
            }
        }
    }
}
