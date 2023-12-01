using MediatR;

namespace OnlineStore.Logic.Concerns.ShoppingCartItemConcern.Put;

public class ShoppingCartItemPutCommand : IRequest<long>
{
    public ShoppingCartItemPutCommand(long id, long userId, ShoppingCartItemPutDto shoppingCartItemPutDto)
    {
        this.Id = id;
        this.UserId = userId;
        this.ShoppingCartItemPutDto = shoppingCartItemPutDto;
    }

    public long Id { get; }

    public long UserId { get; }

    public ShoppingCartItemPutDto ShoppingCartItemPutDto { get; set; }
}
