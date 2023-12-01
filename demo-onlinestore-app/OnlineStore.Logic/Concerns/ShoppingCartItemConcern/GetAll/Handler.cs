using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Database;
using OnlineStore.Database.Entities;

namespace OnlineStore.Logic.Concerns.ShoppingCartItemConcern.GetAll;

public class Handler : IRequestHandler<ShoppingCartItemGetAllCommand, IQueryable<ShoppingCartItemGetAllDto>>
{
    private readonly DataDbContext _dataDbContext;
    private readonly IMapper _mapper;

    public Handler(DataDbContext dataDbContext, IMapper mapper)
    {
        _dataDbContext = dataDbContext;
        _mapper = mapper;
    }

    public Task<IQueryable<ShoppingCartItemGetAllDto>> Handle(ShoppingCartItemGetAllCommand command, CancellationToken cancellationToken)
    {
        var shoppingCartItems = _dataDbContext.Set<ShoppingCartItem>()
            .AsNoTracking()
            .Where(_ => _.UserId == command.UserId && _.PurchaseOrderId == null)
            .ProjectTo<ShoppingCartItemGetAllDto>(_mapper.ConfigurationProvider);

        return Task.FromResult(shoppingCartItems);
    }
}
