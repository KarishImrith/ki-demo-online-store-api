using MediatR;

namespace OnlineStore.Logic.Concerns.ShoppingCartItemConcern.Delete;

public class ShoppingCartItemDeleteCommand : IRequest
{
    public ShoppingCartItemDeleteCommand(long id)
    {
        this.Id = id;
    }

    public long Id { get; }
}
