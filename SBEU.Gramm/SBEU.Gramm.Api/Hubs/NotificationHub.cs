using Microsoft.AspNetCore.SignalR;

namespace SBEU.Gramm.Api.Hubs
{
    public interface INotificationClient
    {
        Task SendNotification(string data);
    }

    public class NotificationHub : Hub<INotificationClient>
    {
        public Task SendAll()
        {
            return Clients.All.SendNotification("Some notification");
        }
    }
}
