using TestRabbitMq.Events;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace TestRabbitMq.Handlers
{
    public class ItemChangedEventConsumer : IConsumer<ItemChangedEventClass>
    {
        readonly ILogger<Startup> _logger;
        public ItemChangedEventConsumer(ILogger<Startup> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<ItemChangedEventClass> context)
        {
            _logger.LogInformation(context.Message.Name + context.Message.Id + ": ItemChangedEvent consumed");
            return Task.CompletedTask;
        }
    }    
}
