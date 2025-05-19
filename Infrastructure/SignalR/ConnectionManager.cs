using System.Collections.Concurrent;
using Domain.SignalR;

namespace Infrastructure.SignalR;

public class ConnectionManager : IConnectionManager
{
    private readonly ConcurrentDictionary<Guid, HashSet<string>> _connections = new();

    public void AddConnection(Guid userId, string connectionId)
    {
        _connections.AddOrUpdate(userId,
            [connectionId], 
            (_, set) => { set.Add(connectionId); return set; });
    }

    public void RemoveConnection(Guid userId, string connectionId)
    {
        if (_connections.TryGetValue(userId, out var set))
        {
            set.Remove(connectionId);
            if (set.Count == 0)
                _connections.TryRemove(userId, out _);
        }
    }

    public IEnumerable<string> GetConnections(Guid userId) => 
        _connections.TryGetValue(userId, out var set) ? set : Enumerable.Empty<string>();
}