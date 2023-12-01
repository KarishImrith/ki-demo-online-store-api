using FluentValidation;
using Ki.Validation.Validators.Abstractions;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Database;
using OnlineStore.Database.Entities;

namespace OnlineStore.Logic.Concerns.ShoppingCartItemConcern.Put;

public class Validator : PipelineValidator<ShoppingCartItemPutCommand>
{
    private readonly DataDbContext _dataDbContext;

    public Validator(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;

        this.RuleFor(_ => _.ShoppingCartItemPutDto.ProductId)
            .MustAsync(ProductIdIsValid);

        this.RuleFor(_ => _.ShoppingCartItemPutDto.Quantity)
            .NotEmpty();
    }

    private async Task<bool> ProductIdIsValid(ShoppingCartItemPutCommand shoppingCartItemPutCommand, long productId, ValidationContext<ShoppingCartItemPutCommand> validationContext, CancellationToken cancellationToken)
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
