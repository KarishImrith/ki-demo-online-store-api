using Ki.Validation.Mediatr.PipelineBehaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Ki.Validation.Mediatr.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediatrPipelineValidation(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        return serviceCollection;
    }
}
