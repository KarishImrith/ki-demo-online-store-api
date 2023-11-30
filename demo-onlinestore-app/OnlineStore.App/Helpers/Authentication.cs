using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace OnlineStore.App.Helpers;

public static class Authentication
{
    public static IServiceCollection AddAuthenticationSupport(this IServiceCollection serviceCollection, string googleOAuth2ClientId)
    {
        // TODO: This breaks identity token authentication since identity tokens are web tokens and this expects bearer tokens to be jwt.
        // NB: This is not fixed now as it is outside of the scope of this project.
        // NB: Cookie authentication with identity still works.
        serviceCollection
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                GoogleDefaults.AuthenticationScheme,
                configureOptions =>
                {
                    configureOptions.Authority = "https://accounts.google.com";
                    configureOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = "https://accounts.google.com",
                        ValidAudience = googleOAuth2ClientId,
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true
                    };
                });

        return serviceCollection;
    }
}
