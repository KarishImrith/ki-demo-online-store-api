using MediatR;

namespace OnlineStore.Logic.Concerns.ProductConcern.GetById;

public class ProductGetByIdCommand : IRequest<ProductGetByIdDto>
{
    public ProductGetByIdCommand(long id)
    {
        this.Id = id;
    }

    public long Id { get; set; }
}
