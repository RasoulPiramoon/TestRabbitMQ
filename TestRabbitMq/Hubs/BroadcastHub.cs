using TestRabbitMq.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRabbitMq.Hubs
{
    public class BroadcastHub : Hub<IBroadcastHub>
    {
        private readonly PresenceTracker presenceTracker;
        
        public BroadcastHub(PresenceTracker presenceTracker)
        {
            this.presenceTracker = presenceTracker;
        }

        public override async Task OnConnectedAsync()
        {
            var httpCtx = Context.GetHttpContext();
            int cashId = 0;
            var cashIdString = httpCtx.Request.Query["cashId"].ToString();

            if (!string.IsNullOrEmpty(cashIdString))
            {
                var res = int.TryParse(cashIdString, out cashId);
            }

            var result = await presenceTracker.ConnectionOpened("1");
            if (result.UserJoined)
            {
                await Clients.All.NewMessage("system", $"{Context.User.Identity.Name} joined");
            }
            var currentUsers = await presenceTracker.GetOnlineUsers();
            await Clients.Caller.NewMessage("system", $"Currently online:\n{string.Join("\n", currentUsers)}");

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var result = await presenceTracker.ConnectionClosed("1");
            if (result.UserLeft)
            {
                await Clients.All.NewMessage("system", $"{Context.User.Identity.Name} left");
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessage(string user, string message)
        {
            if (string.IsNullOrEmpty(user))
                await Clients.All.ReceiveMessageHandler(message);
            else
                await Clients.User(user).ReceiveMessageHandler(message);
        }

        public async Task SendObject(string user)
        {
            List<TestSignalR> obj = new TestSignalR().Get();
            if (string.IsNullOrEmpty(user))
                await Clients.Others.ReceiveObjectHandler(obj);
            else
                await Clients.User(user).ReceiveObjectHandler(obj);
        }
    }
}