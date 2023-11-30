using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Database;
using OnlineStore.Database.Entities;

namespace OnlineStore.Logic.Concerns.ProductConcern.GetAll;

public class Handler : IRequestHandler<ProductGetAllCommand, IQueryable<ProductGetAllDto>>
{
    private readonly DataDbContext _dataDbContext;
    private readonly IMapper _mapper;

    public Handler(DataDbContext dataDbContext, IMapper mapper)
    {
        _dataDbContext = dataDbContext;
        _mapper = mapper;
    }

    public Task<IQueryable<ProductGetAllDto>> Handle(ProductGetAllCommand command, CancellationToken cancellationToken)
    {
        var products = _dataDbContext.Set<Product>()
            .AsNoTracking()
            .ProjectTo<ProductGetAllDto>(_mapper.ConfigurationProvider);

        return Task.FromResult(products);
    }
}
