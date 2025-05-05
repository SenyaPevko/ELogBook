using Microsoft.Extensions.Hosting;

namespace Infrastructure;

public class MongoIndexInitializerHostedService(MongoIndexInitializer initializer) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await initializer.EnsureIndexesAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}