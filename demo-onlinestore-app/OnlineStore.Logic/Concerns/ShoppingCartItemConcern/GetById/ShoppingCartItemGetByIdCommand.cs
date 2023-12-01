using MediatR;

namespace OnlineStore.Logic.Concerns.ShoppingCartItemConcern.GetById;

public class ShoppingCartItemGetByIdCommand : IRequest<ShoppingCartItemGetByIdDto>
{
    public ShoppingCartItemGetByIdCommand(long id, long userId)
    {
        this.Id = id;
        this.UserId = userId;
    }

    public long Id { get; set; }

    public long UserId { get; }
}
