using TestRabbitMq.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRabbitMq.Services
{
    public class SimplePublishService
    {
        private readonly IBus _bus;
        public SimplePublishService(IBus bus)
        {
            _bus = bus;
        }

        public async Task Send(ItemChangedEventClass @event)
        {
            var sendPoint = await _bus.GetSendEndpoint(new Uri("exchange:item-changed-event"));
            await sendPoint.Send<ItemChangedEventClass>(@event);
            //await _bus.Publish<ItemChangedEvent>(@event);
            //await _bus.Send<ItemChangedEvent>(@event);
        }
    }
}
