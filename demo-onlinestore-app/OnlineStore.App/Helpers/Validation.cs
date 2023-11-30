using FluentValidation;
using Ki.Validation.Mediatr.Extensions;
using OnlineStore.Logic.Services;

namespace OnlineStore.App.Helpers;

public static class Validation
{
    public static IServiceCollection AddValidationSupport(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddValidatorsFromAssemblies([typeof(IIdentityService).Assembly])
            .AddMediatrPipelineValidation();

        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

        return serviceCollection;
    }
}
