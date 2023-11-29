using Microsoft.AspNetCore.Identity;

namespace OnlineStore.Database.Entities;

public class User : IdentityUser<long>
{
    // Navigation
    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

    public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
}
