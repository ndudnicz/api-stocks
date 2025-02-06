using dotnet_api.Data.Entities;
using Microsoft.AspNetCore.SignalR;

namespace dotnet_api.Hubs;

public class MessageHub : Hub
{
    public async Task NewMessage(Stock msg) =>
        await Clients.All.SendAsync("messageReceived", msg);

    public async Task TestMessage(string str)
    {
        Console.WriteLine(str);
        await Clients.All.SendAsync("messageReceived", "test");
    }
}