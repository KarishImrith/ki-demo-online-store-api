using AutoFixture;
using OnlineStore.Database.Entities;

namespace OnlineStore.Logic.UnitTests.Helpers;

public static class FixtureExtensions
{
    public static List<Product> GenerateProductEntities(this Fixture fixture, int productCount)
    {
        return fixture.Build<Product>()
            .With(_ => _.Id, (long?)null)
            .Without(_ => _.ProductAttachments)
            .Without(_ => _.ShoppingCartItems)
            .CreateMany(productCount)
            .ToList();
    }

    public static List<ShoppingCartItem> GenerateShoppingCartItemEntities(this Fixture fixture, int shoppingCartItemCount, long userId, long productId)
    {
        return fixture.Build<ShoppingCartItem>()
            .With(_ => _.Id, (long?)null)
            .With(_ => _.UserId, userId)
            .With(_ => _.ProductId, productId)
            .With(_ => _.FinalSellingPrice, (decimal?)null)
            .With(_ => _.PurchaseOrderId, (long?)null)
            .Without(_ => _.User)
            .Without(_ => _.Product)
            .Without(_ => _.PurchaseOrder)
            .CreateMany(shoppingCartItemCount)
            .ToList();
    }
}
