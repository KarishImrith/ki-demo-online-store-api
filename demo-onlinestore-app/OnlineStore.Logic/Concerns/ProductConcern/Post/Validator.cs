using FluentValidation;
using Ki.Validation.Validators.Abstractions;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Database;
using OnlineStore.Database.Entities;

namespace OnlineStore.Logic.Concerns.ProductConcern.Post;

public class Validator : PipelineValidator<ProductPostCommand>
{
    private readonly DataDbContext _dataDbContext;

    public Validator(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;

        this.RuleFor(_ => _.ProductPostDto.Name)
            .MustAsync(NameIsValid);

        this.RuleFor(_ => _.ProductPostDto.CurrentSellingPrice)
            .PrecisionScale(19, 4, true);
    }

    private async Task<bool> NameIsValid(ProductPostCommand productPostCommand, string name, ValidationContext<ProductPostCommand> validationContext, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(name))
        {
            validationContext.AddFailure("'Name' must not be empty.");
            return true;
        }

        var nameLowerCase = name.ToLower();
        var nameExists = await _dataDbContext.Set<Product>().AnyAsync(_ => _.Name.ToLower() == nameLowerCase, cancellationToken);
        if (nameExists)
        {
            validationContext.AddFailure("'Name' is already in use.");
        }

        return true;
    }
}
