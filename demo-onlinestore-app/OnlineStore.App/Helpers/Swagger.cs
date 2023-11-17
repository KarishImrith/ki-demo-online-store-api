using Microsoft.OpenApi.Models;

namespace OnlineStore.App.Helpers;

public static class Swagger
{
    public const string ContactName = "Karish Imrith";
    public const string ContactEmail = "imrith.karish@gmail.com";
    public const string Description = "An ASP.NET Core Web API for managing an online store.";
    public const string EndpointUrl = $"/swagger/{Version}/swagger.json";
    public const string Title = $"{nameof(OnlineStore)} API";
    public const string Version = "v1";

    public static IServiceCollection AddSwaggerSupport(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc(Version, new OpenApiInfo
            {
                Version = Version,
                Title = Title,
                Description = Description,
                Contact = new OpenApiContact
                {
                    Name = ContactName,
                    Email = ContactEmail
                }
            });
        });

        return serviceCollection;
    }

    public static IApplicationBuilder MapSwaggerSupport(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseSwagger();
        applicationBuilder.UseSwaggerUI(setup =>
        {
            setup.RoutePrefix = string.Empty;
            setup.SwaggerEndpoint(EndpointUrl, Title);
        });

        return applicationBuilder;
    }
}
