using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestRabbitMq.Models;
using TestRabbitMq.Events;
using TestRabbitMq.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace TestRabbitMq.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        public IConfiguration configuration { get; }

        public HomeController(ILogger<HomeController> logger, IPublishEndpoint publishEndpoint
            , IConfiguration configuration)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[Authorize]
        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemChangedEventClass item)
        {
            await _publishEndpoint.Publish<ItemChangedEventClass>(item);
            return Json(item);
        }

        [HttpPost]
        public IActionResult Create50000(ItemChangedEventClass item)
        {
            Send(item);
            return Json(item);
        }

        private void Send(ItemChangedEventClass item)
        {
            int length = 50000;
            int pageSize = 2000;
            int partSize = length % pageSize == 0 ? (length / pageSize) : (length / pageSize) + 1;
            int count = 0;
            var items = PrepareList(item, length);

            Task[] tasks = new Task[partSize];
            //publish in multiple thread
            for (int i = 0; i < partSize; i++)
            {
                tasks[i] = publish(items.Skip(count * partSize).Take(pageSize).ToList());
                count++;
            }
            Task.WaitAll(tasks);
        }

        private List<ItemChangedEventClass> PrepareList(ItemChangedEventClass item, int length)
        {
            List<ItemChangedEventClass> items = new List<ItemChangedEventClass>();
            for (int i = 0; i < length; i++)
            {
                items.Add(item);
            }

            return items;
        }

        private async Task publish(List<ItemChangedEventClass> items)
        {
            foreach (var item in items)
            {
                await _publishEndpoint.Publish<ItemChangedEventClass>(item);
            }
        }        
    }
}
