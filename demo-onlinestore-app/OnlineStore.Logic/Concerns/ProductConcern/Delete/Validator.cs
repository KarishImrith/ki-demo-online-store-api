using FluentValidation;
using Ki.Validation.Validators.Abstractions;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Database;
using OnlineStore.Database.Entities;

namespace OnlineStore.Logic.Concerns.ProductConcern.Delete;

public class Validator : PipelineValidator<ProductDeleteCommand>
{
    private readonly DataDbContext _dataDbContext;

    public Validator(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;

        this.RuleFor(_ => _.Id)
            .MustAsync(NoDependantDataExists);
    }

    private async Task<bool> NoDependantDataExists(ProductDeleteCommand productDeleteCommand, long id, ValidationContext<ProductDeleteCommand> validationContext, CancellationToken cancellationToken)
    {
        var productAttachmentsExist = await _dataDbContext.Set<ProductAttachment>().AnyAsync(_ => _.ProductId != id, cancellationToken);
        if (productAttachmentsExist)
        {
            validationContext.AddFailure("'ProductAttachments' exist.");
        }

        var shoppingCartItemsExist = await _dataDbContext.Set<ShoppingCartItem>().AnyAsync(_ => _.ProductId != id, cancellationToken);
        if (shoppingCartItemsExist)
        {
            validationContext.AddFailure("'ShoppingCartItems' exist.");
        }

        return true;
    }
}
