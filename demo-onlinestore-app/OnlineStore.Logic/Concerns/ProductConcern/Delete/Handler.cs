using AutoMapper;
using MediatR;
using OnlineStore.Database;
using OnlineStore.Database.Entities;

namespace OnlineStore.Logic.Concerns.ProductConcern.Delete;

public class Handler : IRequestHandler<ProductDeleteCommand>
{
    private readonly DataDbContext _dataDbContext;
    private readonly IMapper _mapper;

    public Handler(DataDbContext dataDbContext, IMapper mapper)
    {
        _dataDbContext = dataDbContext;
        _mapper = mapper;
    }

    public async Task Handle(ProductDeleteCommand command, CancellationToken cancellationToken)
    {
        var product = await _dataDbContext.Set<Product>().FindAsync([command.Id], cancellationToken: cancellationToken);
        if (product == null)
            throw new KeyNotFoundException();

        _dataDbContext.Remove(product);
        await _dataDbContext.SaveChangesAsync(cancellationToken);
    }
}
