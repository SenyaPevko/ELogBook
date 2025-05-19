using System.Security.Claims;
using Domain.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR;

public class NotificationHub(IConnectionManager connectionManager) : Hub
{
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"OnConnectedAsync");
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(userId is not null && Guid.TryParse(userId, out var userGuidId))
            connectionManager.AddConnection(userGuidId, Context.ConnectionId);
        
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(userId is not null && Guid.TryParse(userId, out var userGuidId))
            connectionManager.RemoveConnection(userGuidId, Context.ConnectionId);
        
        await base.OnDisconnectedAsync(exception);
    }
}

