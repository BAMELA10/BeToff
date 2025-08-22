using Microsoft.AspNetCore.SignalR;
namespace BeToff.Web.Hubs;
public class NotificationHub : Hub
{

    public async Task TestMessage()
    {
        await Clients.All.SendAsync("Default", "HelloWorld");

    }
}

