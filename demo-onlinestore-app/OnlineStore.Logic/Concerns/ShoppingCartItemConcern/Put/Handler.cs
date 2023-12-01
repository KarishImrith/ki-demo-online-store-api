using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Database;
using OnlineStore.Database.Entities;

namespace OnlineStore.Logic.Concerns.ShoppingCartItemConcern.Put;

public class Handler : IRequestHandler<ShoppingCartItemPutCommand, long>
{
    private readonly DataDbContext _dataDbContext;
    private readonly IMapper _mapper;

    public Handler(DataDbContext dataDbContext, IMapper mapper)
    {
        _dataDbContext = dataDbContext;
        _mapper = mapper;
    }

    public async Task<long> Handle(ShoppingCartItemPutCommand command, CancellationToken cancellationToken)
    {
        var shoppingCartItem = await _dataDbContext.Set<ShoppingCartItem>().SingleOrDefaultAsync(_ => _.Id == command.Id && _.UserId == command.UserId, cancellationToken: cancellationToken);
        if (shoppingCartItem == null)
            throw new KeyNotFoundException();

        _mapper.Map(command.ShoppingCartItemPutDto, shoppingCartItem);

        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return shoppingCartItem.Id;
    }
}
