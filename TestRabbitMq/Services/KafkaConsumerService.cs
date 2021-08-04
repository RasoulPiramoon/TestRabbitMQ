using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace TestRabbitMq.Services
{
    public class KafkaConsumerService : BackgroundService
    {
        //private readonly ILogger<KafkaConsumerService> _logger;
        private readonly string topic;
        private readonly IConsumer<string, long> kafkaConsumer;
        ConsumerConfig consumerConfig = new ConsumerConfig();

        public KafkaConsumerService(IConfiguration config)
        {
            config.GetSection("Kafka:ConsumerSettings").Bind(consumerConfig);
            topic = config.GetValue<string>("Kafka:ItemTopic");
            //this.kafkaConsumer = new ConsumerBuilder<string, long>(consumerConfig).Build();
        }


        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using (var kafkaConsumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build())
            {
                kafkaConsumer.Subscribe(topic);
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var cr = kafkaConsumer.Consume(cancellationToken);
                        Console.WriteLine(cr.Message.Value);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (ConsumeException e)
                    {
                        // Consumer errors should generally be ignored (or logged) unless fatal.
                        Console.WriteLine($"Consume error: {e.Error.Reason}");

                        if (e.Error.IsFatal)
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                kafkaConsumer.Close();
            }

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            this.kafkaConsumer.Close(); // Commit offsets and leave the group cleanly.
            this.kafkaConsumer.Dispose();

            base.Dispose();
        }
    }
}
