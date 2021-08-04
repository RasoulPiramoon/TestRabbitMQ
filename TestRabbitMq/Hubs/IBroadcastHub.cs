using System.Threading.Tasks;

namespace TestRabbitMq.Hubs
{
    public interface IBroadcastHub
    {
        Task NewDataExists();
        Task NewMessage(string name, string message);
        Task ReceiveMessageHandler(string message);
        Task ReceiveObjectHandler(object obj);
    }
}