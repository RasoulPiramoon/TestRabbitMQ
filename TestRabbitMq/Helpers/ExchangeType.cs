using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRabbitMq.Helpers
{
    public enum ExchangeType
    {
        Unknown = 0,
        Direct = 1,
        Fanout = 2,
        Headers = 3,
        Topic = 4
    }
}
