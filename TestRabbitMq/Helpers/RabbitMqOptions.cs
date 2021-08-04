using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRabbitMq.Helpers
{
    public class RabbitMqOptions
    {
        public RabbitMqOptions()
        { }

        public string Namespace { get; set; }
        public int Retries { get; set; }
        public int RetryInterval { get; set; }
        public static RabbitMqOptions Local { get; }
        public TimeSpan PublishConfirmTimeout { get; set; }
        public List<string> Hostnames { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string VirtualHost { get; set; }
        public SslOption Ssl { get; set; }
        public TimeSpan RequestTimeout { get; set; }
        public bool AutoCloseConnection { get; set; }
        public GeneralQueueConfiguration Queue { get; set; }
        public GeneralExchangeConfiguration Exchange { get; set; }
        public bool TopologyRecovery { get; set; }
        public bool AutomaticRecovery { get; set; }
        public bool RouteWithGlobalId { get; set; }
        public TimeSpan GracefulShutdown { get; set; }
        public TimeSpan RecoveryInterval { get; set; }
        public bool PersistentDeliveryMode { get; set; }
        public int MinInterval { get; set; }
        public int MaxInterval { get; set; }
        public int IntervalDelta { get; set; }
    }
}
