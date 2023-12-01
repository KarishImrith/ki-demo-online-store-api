using MediatR;

namespace OnlineStore.Logic.Concerns.ShoppingCartItemConcern.GetAll;

public class ShoppingCartItemGetAllCommand : IRequest<IQueryable<ShoppingCartItemGetAllDto>>
{
    public ShoppingCartItemGetAllCommand(long userId)
    {
        this.UserId = userId;
    }

    public long UserId { get; }
}
