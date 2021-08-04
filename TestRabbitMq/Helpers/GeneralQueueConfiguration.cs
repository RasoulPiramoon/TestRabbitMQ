using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRabbitMq.Helpers
{
    public class GeneralQueueConfiguration
    {
        public GeneralQueueConfiguration()
        { }

        public bool AutoDelete { get; set; }
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
    }
}
