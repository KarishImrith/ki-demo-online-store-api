using FluentValidation;
using Ki.Validation.Validators.Abstractions;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Database;
using OnlineStore.Database.Entities;

namespace OnlineStore.Logic.Concerns.ShoppingCartItemConcern.Delete;

public class Validator : PipelineValidator<ShoppingCartItemDeleteCommand>
{
    private readonly DataDbContext _dataDbContext;

    public Validator(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;

        this.RuleFor(_ => _.Id)
            .MustAsync(NoDependantDataExists);
    }

    private async Task<bool> NoDependantDataExists(ShoppingCartItemDeleteCommand productDeleteCommand, long id, ValidationContext<ShoppingCartItemDeleteCommand> validationContext, CancellationToken cancellationToken)
    {
        var purchaseOrderExists = await _dataDbContext.Set<ShoppingCartItem>().AnyAsync(_ => _.Id == id && _.PurchaseOrderId != null, cancellationToken);
        if (purchaseOrderExists)
        {
            validationContext.AddFailure("'PurchaseOrder' exists.");
        }

        return true;
    }
}
