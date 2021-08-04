using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRabbitMq.Models
{
    public class TestSignalR
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public int SellPrice { get; set; }
        public int BuyPrice { get; set; }
        public int DiscountPercent { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }

        public List<TestSignalR> Get()
        {
            List<TestSignalR> list = new List<TestSignalR>();
            for (int i = 1; i < 51; i++)
            {
                list.Add(new TestSignalR { Id = i, Code=i, BuyPrice = i*1
                    , SellPrice = i*3/2, Qty = i, DiscountPercent=i, Name = $"Name {i}"
                    , CreatedBy = Guid.NewGuid(), CreatedOn = DateTime.Now });
            }

            return list;
        }
    }
}
