using AutoMapper;
using MediatR;
using OnlineStore.Database;
using OnlineStore.Database.Entities;

namespace OnlineStore.Logic.Concerns.ShoppingCartItemConcern.Post;

public class Handler : IRequestHandler<ShoppingCartItemPostCommand, long>
{
    private readonly DataDbContext _dataDbContext;
    private readonly IMapper _mapper;

    public Handler(DataDbContext dataDbContext, IMapper mapper)
    {
        _dataDbContext = dataDbContext;
        _mapper = mapper;
    }

    public async Task<long> Handle(ShoppingCartItemPostCommand command, CancellationToken cancellationToken)
    {
        var shoppingCartItem = _mapper.Map<ShoppingCartItem>(command.ShoppingCartItemPostDto);
        shoppingCartItem.UserId = command.UserId;

        await _dataDbContext.AddAsync(shoppingCartItem, cancellationToken);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return shoppingCartItem.Id;
    }
}
