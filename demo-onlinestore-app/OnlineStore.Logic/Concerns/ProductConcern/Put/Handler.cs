using AutoMapper;
using MediatR;
using OnlineStore.Database;
using OnlineStore.Database.Entities;

namespace OnlineStore.Logic.Concerns.ProductConcern.Put;

public class Handler : IRequestHandler<ProductPutCommand>
{
    private readonly DataDbContext _dataDbContext;
    private readonly IMapper _mapper;

    public Handler(DataDbContext dataDbContext, IMapper mapper)
    {
        _dataDbContext = dataDbContext;
        _mapper = mapper;
    }

    public async Task Handle(ProductPutCommand command, CancellationToken cancellationToken)
    {
        var product = await _dataDbContext.Set<Product>().FindAsync([command.Id], cancellationToken: cancellationToken);
        if (product == null)
            throw new KeyNotFoundException();

        _mapper.Map(command.ProductPutDto, product);

        await _dataDbContext.SaveChangesAsync(cancellationToken);
    }
}
