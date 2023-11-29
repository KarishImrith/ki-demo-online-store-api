using OnlineStore.App.Helpers;
using OnlineStore.Database.Entities;
using static OnlineStore.App.Constants;

var builder = WebApplication.CreateBuilder(args);
var host = builder.Host;
var services = builder.Services;
var configuration = builder.Configuration;

/* Configure logging. */
host.ConfigureSerilogSupport();

/* Add services to the container. */
services.AddAuthorization();
services.AddControllers();
services.AddDatabaseSupport(configuration.GetValue<string>(SecretKeyConstants.SqlServerDatabaseConnectionString));
services.AddHealthChecks();
services.AddIdentitySupport();

services.AddSwaggerSupport(
    configuration.GetValue<string>(SecretKeyConstants.SwaggerContactName),
    configuration.GetValue<string>(SecretKeyConstants.SwaggerContactEmailAddress));

var app = builder.Build();

/* Configure the HTTP request pipeline. */
app.MapControllers();
app.MapHealthCheckSupport();
app.MapIdentityApi<User>();
app.MapSwaggerSupport();

app.Services.EnsureDatabaseMigrationsApplied();

app.Run();
