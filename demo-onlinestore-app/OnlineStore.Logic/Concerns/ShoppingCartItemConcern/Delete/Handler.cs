using AutoMapper;
using MediatR;
using OnlineStore.Database;
using OnlineStore.Database.Entities;

namespace OnlineStore.Logic.Concerns.ShoppingCartItemConcern.Delete;

public class Handler : IRequestHandler<ShoppingCartItemDeleteCommand>
{
    private readonly DataDbContext _dataDbContext;
    private readonly IMapper _mapper;

    public Handler(DataDbContext dataDbContext, IMapper mapper)
    {
        _dataDbContext = dataDbContext;
        _mapper = mapper;
    }

    public async Task Handle(ShoppingCartItemDeleteCommand command, CancellationToken cancellationToken)
    {
        var shoppingCartItem = await _dataDbContext.Set<ShoppingCartItem>().FindAsync([command.Id], cancellationToken: cancellationToken);
        if (shoppingCartItem == null)
            throw new KeyNotFoundException();

        _dataDbContext.Remove(shoppingCartItem);
        await _dataDbContext.SaveChangesAsync(cancellationToken);
    }
}
