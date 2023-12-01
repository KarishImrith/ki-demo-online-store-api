using MediatR;

namespace OnlineStore.Logic.Concerns.ShoppingCartItemConcern.Post;

public class ShoppingCartItemPostCommand : IRequest<long>
{
    public ShoppingCartItemPostCommand(long userId, ShoppingCartItemPostDto shoppingCartItemPostDto)
    {
        this.UserId = userId;
        this.ShoppingCartItemPostDto = shoppingCartItemPostDto;
    }

    public long UserId { get; }

    public ShoppingCartItemPostDto ShoppingCartItemPostDto { get; set; }
}
