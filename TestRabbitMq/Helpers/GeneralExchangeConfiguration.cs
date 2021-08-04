using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRabbitMq.Helpers
{
    public class GeneralExchangeConfiguration
    {
        public GeneralExchangeConfiguration() { }

        public bool Durable { get; set; }
        public bool AutoDelete { get; set; }
        public ExchangeType Type { get; set; }
    }
}
