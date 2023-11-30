using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace OnlineStore.App.Helpers;

public static class Authorization
{
    public static IServiceCollection AddAuthorizationSupport(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddAuthorization(configure =>
            {
                configure.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(
                        IdentityConstants.ApplicationScheme,
                        GoogleDefaults.AuthenticationScheme)
                    .Build();
            });

        return serviceCollection;
    }
}
