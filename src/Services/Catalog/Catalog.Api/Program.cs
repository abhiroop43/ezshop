var builder = WebApplication.CreateBuilder(args);

Env.TraversePath().Load();
var assembly = typeof(Program).Assembly;

// add services to the container
builder.Services.AddMediatR(cfg =>
{
    cfg.LicenseKey = Env.GetString("MEDIATR_LICENSE_KEY");
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder
    .Services.AddMarten(opts => { opts.Connection(builder.Configuration.GetConnectionString("Database")!); })
    .UseLightweightSessions();

if (builder.Environment.IsDevelopment()) builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddCarter();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// configure the HTTP request pipeline
app.MapCarter();
app.UseExceptionHandler(opts => { });

await app.RunAsync();