var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// add services to the container

app.MapGet("/", () => "Hello World!");

// configure the HTTP request pipeline

await app.RunAsync();