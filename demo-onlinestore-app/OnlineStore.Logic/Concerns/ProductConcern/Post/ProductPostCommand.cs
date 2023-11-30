using MediatR;

namespace OnlineStore.Logic.Concerns.ProductConcern.Post;

public class ProductPostCommand : IRequest<long>
{
    public ProductPostCommand(ProductPostDto productPostDto)
    {
        this.ProductPostDto = productPostDto;
    }

    public ProductPostDto ProductPostDto { get; set; }
}
