using FluentValidation;
using Ki.Validation.Validators.Abstractions;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Database;
using OnlineStore.Database.Entities;

namespace OnlineStore.Logic.Concerns.ShoppingCartItemConcern.Post;

public class Validator : PipelineValidator<ShoppingCartItemPostCommand>
{
    private readonly DataDbContext _dataDbContext;

    public Validator(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;

        this.RuleFor(_ => _.ShoppingCartItemPostDto.ProductId)
            .MustAsync(ProductIdIsValid);

        this.RuleFor(_ => _.ShoppingCartItemPostDto.Quantity)
            .NotEmpty();
    }

    private async Task<bool> ProductIdIsValid(ShoppingCartItemPostCommand shoppingCartItemPostCommand, long productId, ValidationContext<ShoppingCartItemPostCommand> validationContext, CancellationToken cancellationToken)
    {
        if (productId == default)
        {
            validationContext.AddFailure("'ProductId' must not be null.");
            return true;
        }

        var productIdExists = await _dataDbContext.Set<Product>().AnyAsync(_ => _.Id == productId, cancellationToken);
        if (!productIdExists)
        {
            validationContext.AddFailure("'ProductId' does not exist.");
        }

        return true;
    }
}
