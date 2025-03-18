using System.Text.Json;
using System.Text.Json.Serialization;
using dotnet_api.Hubs;
using dotnet_api.Jobs;
using dotnet_api.Repositories;
using dotnet_api.Services;
using Hangfire;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddSignalR();
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
});
builder.Services.AddRouting(opt => opt.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});
builder.Services.AddSingleton<MessageHub>();
builder.Services.AddTransient<IStockRepository, StockRepository>();
builder.Services.AddTransient<IStockService, StockService>();
builder.Services.AddTransient<IScrapperJob, ScrapperJob>();
builder.Services.AddHangfire(c => c.UseInMemoryStorage());
builder.Services.AddHangfireServer();

HubConnectionHandler hubConnectionHandler = new HubConnectionHandler(configuration?["SignalR:Endpoint"] + configuration?["SignalR:Route"]);
hubConnectionHandler.StartAsync();

builder.Services.AddSingleton<IHubConnectionHandler>(hubConnectionHandler);

var app = builder.Build();
app.UseCors(b =>
        b.WithOrigins(["http://localhost:4200", "http://localhost:5173", "http://localhost:3000"])
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()  // Add AllowCredentials for SignalR to work with cookies or authorization headers
);
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<MessageHub>(configuration?["SignalR:Route"]);
});
app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseHangfireDashboard("/hangfire");
string[] stocksIsin = configuration?["StocksIsin"].Split(',');
RecurringJob.AddOrUpdate<IScrapperJob>("scrapper", job => job.Run(stocksIsin), Cron.Minutely);
// BackgroundJob.Enqueue(() => app.Services.GetRequiredService<IScrapperJob>().Run(stocksIsin));
app.Run();
