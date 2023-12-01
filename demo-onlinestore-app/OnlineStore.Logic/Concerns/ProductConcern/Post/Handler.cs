using AutoMapper;
using MediatR;
using OnlineStore.Database;
using OnlineStore.Database.Entities;

namespace OnlineStore.Logic.Concerns.ProductConcern.Post;

public class Handler : IRequestHandler<ProductPostCommand, long>
{
    private readonly DataDbContext _dataDbContext;
    private readonly IMapper _mapper;

    public Handler(DataDbContext dataDbContext, IMapper mapper)
    {
        _dataDbContext = dataDbContext;
        _mapper = mapper;
    }

    public async Task<long> Handle(ProductPostCommand command, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(command.ProductPostDto);

        await _dataDbContext.AddAsync(product, cancellationToken);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
