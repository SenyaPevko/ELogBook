using Domain.Settings;
using ELogBook;
using ELogBook.Middleware;
using Infrastructure.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSwagger()
    .AddApplicationServices(builder.Configuration)
    .AddAuthenticationAndAuthorization(builder.Configuration)
    .AddMongoDb(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseHttpsRedirection();
app.UseCors("SignalRPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<RequestContextMiddleware>();
app.MapControllers();
app.MapHub<NotificationHub>(SignalrSettings.NotificationHubPath)
    .RequireAuthorization()
    .WithOpenApi();
app.Run();