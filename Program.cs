using System.Text.Json;
using System.Text.Json.Serialization;
using dotnet_api.Jobs;
using dotnet_api.Repositories;
using dotnet_api.Services;
using Hangfire;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var cors = "cors";
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(new CorsPolicyBuilder().AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod().Build());
    options.AddPolicy(cors,
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});

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
builder.Services.AddTransient<IElementRepository, ElementRepository>();
builder.Services.AddTransient<IElementService, ElementService>();
builder.Services.AddTransient<IScrapperJob, ScrapperJob>();
builder.Services.AddHangfire(c => c.UseInMemoryStorage());
builder.Services.AddHangfireServer();
var configuration = builder.Configuration;

var app = builder.Build();
app.UseRouting();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
app.UseCors(cors);
app.UseHangfireDashboard("/hangfire");
string[] stocks = configuration?["Stocks"].Split(',');
RecurringJob.AddOrUpdate<IScrapperJob>("scrapper", job => job.Run(stocks), Cron.Minutely);
app.Run();
