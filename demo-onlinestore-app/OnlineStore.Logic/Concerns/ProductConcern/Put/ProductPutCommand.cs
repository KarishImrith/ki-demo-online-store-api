using MediatR;

namespace OnlineStore.Logic.Concerns.ProductConcern.Put;

public class ProductPutCommand : IRequest
{
    public ProductPutCommand(long id, ProductPutDto productPutDto)
    {
        this.Id = id;
        this.ProductPutDto = productPutDto;
    }

    public long Id { get; }

    public ProductPutDto ProductPutDto { get; set; }
}
