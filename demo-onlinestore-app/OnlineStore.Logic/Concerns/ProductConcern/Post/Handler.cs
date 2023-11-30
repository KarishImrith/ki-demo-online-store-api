using AutoMapper;
using MediatR;
using OnlineStore.Database;
using OnlineStore.Database.Entities;

namespace OnlineStore.Logic.Concerns.ProductConcern.Post;

public class Handler : IRequestHandler<ProductPostCommand>
{
    private readonly DataDbContext _dataDbContext;
    private readonly IMapper _mapper;

    public Handler(DataDbContext dataDbContext, IMapper mapper)
    {
        _dataDbContext = dataDbContext;
        _mapper = mapper;
    }

    public async Task Handle(ProductPostCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request.ProductPostDto);

        await _dataDbContext.AddAsync(product, cancellationToken);
        await _dataDbContext.SaveChangesAsync();
    }
}
