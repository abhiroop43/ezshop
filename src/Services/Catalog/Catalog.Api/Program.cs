using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.TraversePath().Load();

// add services to the container
builder.Services.AddMediatR(cfg =>
{
    cfg.LicenseKey = Env.GetString("MEDIATR_LICENSE_KEY");
});

var app = builder.Build();

// configure the HTTP request pipeline

await app.RunAsync();
