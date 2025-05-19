namespace Domain.SignalR;

public interface IConnectionManager
{
    void AddConnection(Guid userId, string connectionId);
    void RemoveConnection(Guid userId, string connectionId);
    IEnumerable<string> GetConnections(Guid userId);
}