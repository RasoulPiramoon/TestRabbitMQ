using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TestRabbitMq.Events;
using TestRabbitMq.Handlers;
using TestRabbitMq.Hubs;
using TestRabbitMq.Services;
using TestRabbitMq.Helpers;
using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace TestRabbitMq
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //services.AddSignalR();
            //services.AddScoped<SimplePublishService>();
            //services.AddSingleton<PresenceTracker>();
            //services.AddHostedService<KafkaConsumerService>();

            services.AddMassTransit(x =>
            {
                var rabitConfig = Configuration.GetOptions<RabbitMqOptions>("rabbitMq");
                x.SetKebabCaseEndpointNameFormatter();
                //var entryAssembly = Assembly.GetEntryAssembly();
                x.AddConsumer<ItemChangedEventConsumer>();
                ILogger<Startup> logger = addIlogger(services);
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri("rabbitmq://" + rabitConfig.Hostnames[0] + ":" + rabitConfig.Port + rabitConfig.VirtualHost)
                        , h =>
                    {
                        h.Username(rabitConfig.Username);
                        h.Password(rabitConfig.Password);
                    });

                    cfg.Message<ItemChangedEventClass>(x => x.SetEntityName("item-changed"));
                    cfg.Send<ItemChangedEventClass>(x => { x.UseRoutingKeyFormatter(context => context.Message.ClientId.ToString()); });
                    cfg.Publish<ItemChangedEventClass>(x => { x.ExchangeType = RabbitMQ.Client.ExchangeType.Direct; });
                    cfg.ReceiveEndpoint("item-changed" + Configuration.GetSection("ClientId").Value, re =>
                    {
                         re.ConfigureConsumeTopology = false;
                         //re.Consumer(() => new ItemChangedEventConsumer(logger));
                         re.ConfigureConsumer<ItemChangedEventConsumer>(context);
                         re.UseMessageRetry(r => r.Exponential(rabitConfig.Retries, TimeSpan.FromSeconds(rabitConfig.MinInterval), 
                                    TimeSpan.FromSeconds(rabitConfig.MaxInterval), TimeSpan.FromSeconds(rabitConfig.IntervalDelta)));
                         re.Bind("item-changed", e =>
                         {
                             e.RoutingKey = Configuration.GetSection("ClientId").Value; //TODO
                             e.ExchangeType = RabbitMQ.Client.ExchangeType.Direct;
                         });
                    });
                });
            });
            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapHub<BroadcastHub>("/broadcasthub");
            });
        }

        private ILogger<Startup> addIlogger(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<Startup>>();
            services.AddSingleton(typeof(ILogger), logger);

            return logger;
        }
    }
}
