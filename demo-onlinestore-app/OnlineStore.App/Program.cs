using OnlineStore.App.Helpers;

var builder = WebApplication.CreateBuilder(args);

/* Configure logging. */
builder.Host.ConfigureSerilog();

/* Add services to the container. */
builder.Services.AddControllers();
builder.Services.AddHealthChecks();

var app = builder.Build();

/* Configure the HTTP request pipeline. */
app.MapControllers();
app.MapHealthCheckEndpoints();

app.Run();
