using MediatR;

namespace OnlineStore.Logic.Concerns.ProductConcern.Delete;

public class ProductDeleteCommand : IRequest
{
    public ProductDeleteCommand(long id)
    {
        this.Id = id;
    }

    public long Id { get; }
}
