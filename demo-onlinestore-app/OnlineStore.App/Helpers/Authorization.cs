using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using OnlineStore.Logic.Services;
using System.Security.Authentication;
using System.Security.Claims;
using static OnlineStore.App.Constants;

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

    public static IApplicationBuilder UseAuthorizationSupport(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseAuthorization();

        applicationBuilder.Use(async (httpContext, next) =>
        {
            var currentUserEmailAddress = httpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            if (!string.IsNullOrEmpty(currentUserEmailAddress))
            {
                using var scope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var identityService = scope.ServiceProvider.GetRequiredService<IIdentityService>();

                var currentUserId = identityService.GetUserId(currentUserEmailAddress);
                if (currentUserId == default)
                    throw new AuthenticationException();

                httpContext.Items[HttpContextItemKeyConstants.UserId] = currentUserId;
            }

            await next.Invoke();
        });

        return applicationBuilder;
    }
}
