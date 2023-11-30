using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Database;
using OnlineStore.Database.Entities;

namespace OnlineStore.Logic.Concerns.ProductConcern.GetById;

public class Handler : IRequestHandler<ProductGetByIdCommand, ProductGetByIdDto>
{
    private readonly DataDbContext _dataDbContext;
    private readonly IMapper _mapper;

    public Handler(DataDbContext dataDbContext, IMapper mapper)
    {
        _dataDbContext = dataDbContext;
        _mapper = mapper;
    }

    public async Task<ProductGetByIdDto> Handle(ProductGetByIdCommand command, CancellationToken cancellationToken)
    {
        var product = await _dataDbContext.Set<Product>()
            .AsNoTracking()
            .ProjectTo<ProductGetByIdDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(_ => _.Id == command.Id, cancellationToken);

        if (product == null)
            throw new KeyNotFoundException();

        return product;
    }
}
