using Order.Api;
using Order.Application;
using Order.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AppApiServices();

var app = builder.Build();

// Configure the HTTP request pipeline.

await app.RunAsync();