var builder = WebApplication.CreateBuilder(args);

// add services to the container

var app = builder.Build();

// configure the HTTP request pipeline

await app.RunAsync();