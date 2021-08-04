//using GWA.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRabbitMq.Events
{
    public class ItemChangedEventClass 
    {
        public ItemChangedEventClass()
        {}

        public long Id { get; set; }
        public int Code { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Comment { get; set; }
        public long ClientId { get; set; }
    }
}
