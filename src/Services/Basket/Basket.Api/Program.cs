
var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

// add services to the container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

var app = builder.Build();

// configure the HTTP Request pipeline
app.MapCarter();

await app.RunAsync();