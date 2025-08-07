using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.TraversePath().Load();

// add services to the container
builder.Services.AddMediatR(cfg =>
{
    cfg.LicenseKey = Env.GetString("MEDIATR_LICENSE_KEY");
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder
    .Services.AddMarten(opts =>
    {
        opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    })
    .UseLightweightSessions();

builder.Services.AddCarter();

var app = builder.Build();

// configure the HTTP request pipeline
app.MapCarter();

await app.RunAsync();
