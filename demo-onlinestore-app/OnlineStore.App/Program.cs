using OnlineStore.App;
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
services.AddAuthenticationSupport(configuration.GetValue<string>(SecretKeyConstants.GoogleOAuth2ClientId));
services.AddAuthorizationSupport();
services.AddAutoMapperSupport();

services
    .AddControllersSupport()
    .AddODataSupport(configuration.GetConfig<AppOptions>().ODataRoutePrefix, configuration.GetConfig<AppOptions>().ODataMaxTop);

services.AddDatabaseSupport(configuration.GetValue<string>(SecretKeyConstants.SqlServerDatabaseConnectionString));
services.AddHealthChecks();
services.AddIdentitySupport();
services.AddLogicSupport();
services.AddMediatRSupport();

services.AddSwaggerSupport(
    configuration.GetValue<string>(SecretKeyConstants.SwaggerContactName),
    configuration.GetValue<string>(SecretKeyConstants.SwaggerContactEmailAddress));

var app = builder.Build();

/* Configure the HTTP request pipeline. */
app.UseAuthentication();
app.UseAuthorizationSupport();

app.MapControllers();
app.MapHealthCheckSupport();
app.MapIdentityApi<User>();
app.MapSwaggerSupport();

app.Services.EnsureDatabaseMigrationsApplied();

app.Run();
