using OnlineStore.App.Helpers;
using OnlineStore.Database.Entities;
using static OnlineStore.App.Constants;

var builder = WebApplication.CreateBuilder(args);

/* Configure logging. */
builder.Host.ConfigureSerilogSupport();

/* Add services to the container. */
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddDatabaseSupport();
builder.Services.AddHealthChecks();
builder.Services.AddIdentitySupport();

builder.Services.AddSwaggerSupport(
    builder.Configuration.GetValue<string>(SecretKeyConstants.SwaggerContactName),
    builder.Configuration.GetValue<string>(SecretKeyConstants.SwaggerContactEmailAddress));

var app = builder.Build();

/* Configure the HTTP request pipeline. */
app.MapControllers();
app.MapHealthCheckSupport();
app.MapIdentityApi<User>();
app.MapSwaggerSupport();

app.Run();
