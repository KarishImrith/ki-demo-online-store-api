using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Database;
using OnlineStore.Database.Entities;

namespace OnlineStore.Logic.Concerns.ShoppingCartItemConcern.GetById;

public class Handler : IRequestHandler<ShoppingCartItemGetByIdCommand, ShoppingCartItemGetByIdDto>
{
    private readonly DataDbContext _dataDbContext;
    private readonly IMapper _mapper;

    public Handler(DataDbContext dataDbContext, IMapper mapper)
    {
        _dataDbContext = dataDbContext;
        _mapper = mapper;
    }

    public async Task<ShoppingCartItemGetByIdDto> Handle(ShoppingCartItemGetByIdCommand command, CancellationToken cancellationToken)
    {
        var shoppingCartItem = await _dataDbContext.Set<ShoppingCartItem>()
            .AsNoTracking()
            .ProjectTo<ShoppingCartItemGetByIdDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(_ => _.Id == command.Id && _.UserId == command.UserId, cancellationToken);

        if (shoppingCartItem == null)
            throw new KeyNotFoundException();

        return shoppingCartItem;
    }
}
