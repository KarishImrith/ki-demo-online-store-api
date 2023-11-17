using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using static OnlineStore.App.Constants;

namespace OnlineStore.App.Helpers;

public static class Swagger
{
    public static IServiceCollection AddSwaggerSupport(this IServiceCollection serviceCollection, string contactName, string contactEmailAddress)
    {
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc(SwaggerConstants.Version, new OpenApiInfo
            {
                Version = SwaggerConstants.Version,
                Title = SwaggerConstants.DocumentTitle,
                Description = SwaggerConstants.DocumentDescription,
                Contact = new OpenApiContact
                {
                    Name = contactName,
                    Email = contactEmailAddress
                }
            });

            setup.AddSecurityDefinition(
                SwaggerConstants.SecurityDefinitionOAuth2,
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = HeaderNames.Authorization,
                    Type = SecuritySchemeType.ApiKey
                });

            setup.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        return serviceCollection;
    }

    public static IApplicationBuilder MapSwaggerSupport(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseSwagger();
        applicationBuilder.UseSwaggerUI(setup =>
        {
            setup.RoutePrefix = string.Empty;
            setup.SwaggerEndpoint(SwaggerConstants.EndpointUrl, SwaggerConstants.DocumentTitle);
        });

        return applicationBuilder;
    }
}
