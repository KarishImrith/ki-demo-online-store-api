using FluentValidation;
using Ki.Validation.Validators.Abstractions;
using MediatR;

namespace Ki.Validation.Mediatr.PipelineBehaviors;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any(_ => _.GetType().IsAssignableTo(typeof(PipelineValidator<TRequest>))))
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(_ => _.ValidateAsync(context, cancellationToken)));

            var errors = validationResults.SelectMany(_ => _.Errors)
                .Where(_ => _ != null)
                .ToList();

            if (errors.Count != 0)
                throw new ValidationException(errors);
        }

        return await next();
    }
}
