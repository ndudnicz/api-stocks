using Microsoft.AspNetCore.SignalR.Client;

namespace dotnet_api.Hubs;

public interface IHubConnectionHandler
{
    HubConnection HubConnection { get; }
    Task StartAsync();
    Task StopASync();
}

public class HubConnectionHandler : IHubConnectionHandler
{
    private readonly string _signalrEndpoint;
    private readonly HubConnection _hubConnection;

    public HubConnection HubConnection { get { return _hubConnection; } }

    public string SignalREndpoint { get { return _signalrEndpoint; } }

    public HubConnectionHandler(string endpoint)
    {
        _signalrEndpoint = endpoint;
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(endpoint)
            .WithAutomaticReconnect()
            .Build();
    }

    public async Task StartAsync()
    {
        try
        {
            Console.WriteLine($"HubConnectionHandler.StartAsync signalR trying to connect to {_signalrEndpoint}");
            await _hubConnection?.StartAsync();
            Console.WriteLine($"HubConnectionHandler.StartAsync signalR connected to {_signalrEndpoint}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"HubConnectionHandler.StartAsync Error starting connection (retry in 3 s.): {ex.Message}");
            await Task.Delay(3000);
            await StartAsync();
        }
    }

    public async Task StopASync()
    {
        await _hubConnection?.StopAsync();
    }
}