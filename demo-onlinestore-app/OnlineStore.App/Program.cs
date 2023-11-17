using OnlineStore.App.Helpers;

var builder = WebApplication.CreateBuilder(args);

/* Configure logging. */
builder.Host.ConfigureSerilogSupport();

/* Add services to the container. */
builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerSupport();

var app = builder.Build();

/* Configure the HTTP request pipeline. */
app.MapControllers();
app.MapHealthCheckSupport();
app.MapSwaggerSupport();

app.Run();
