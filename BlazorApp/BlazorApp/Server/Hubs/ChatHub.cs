using Microsoft.AspNetCore.SignalR;

namespace BlazorApp.Server.Hubs;

public class ChatHub : Hub
{
    private Dictionary<string, string> users = new();

    public override async Task OnConnectedAsync()
    {
        string username = Context.GetHttpContext().Request.Query["username"];
        users.Add(Context.ConnectionId, username);
        await AddMessageToHub(String.Empty, $"{username} connected!");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        string username = Context.GetHttpContext().Request.Query["username"];;
        await AddMessageToHub(String.Empty, $"{username} disconnected!");
    }

    public async Task AddMessageToHub(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}