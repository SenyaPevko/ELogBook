using Domain.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR;

[Authorize]
public class NotificationHub(IConnectionManager connectionManager) : Hub
{
    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        if(userId is not null && Guid.TryParse(userId, out var userGuidId))
            connectionManager.AddConnection(userGuidId, Context.ConnectionId);
        
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.UserIdentifier;
        if(userId is not null && Guid.TryParse(userId, out var userGuidId))
            connectionManager.RemoveConnection(userGuidId, Context.ConnectionId);
        
        await base.OnDisconnectedAsync(exception);
    }
}

