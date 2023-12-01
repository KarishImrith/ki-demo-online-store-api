using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.RateLimiting;
using OnlineStore.App.Helpers;
using OnlineStore.Logic.Concerns.ShoppingCartItemConcern.Delete;
using OnlineStore.Logic.Concerns.ShoppingCartItemConcern.GetAll;
using OnlineStore.Logic.Concerns.ShoppingCartItemConcern.GetById;
using OnlineStore.Logic.Concerns.ShoppingCartItemConcern.Post;
using OnlineStore.Logic.Concerns.ShoppingCartItemConcern.Put;

namespace OnlineStore.App.Controllers;

[Authorize]
[EnableRateLimiting(nameof(OnlineStore))]
public class ShoppingCartItemController : ODataController
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ShoppingCartItemController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpDelete]
    public Task Delete(long key, CancellationToken cancellationToken)
        => _mediator.Send(new ShoppingCartItemDeleteCommand(key), cancellationToken);

    [HttpGet]
    [EnableQuery]
    public Task<IQueryable<ShoppingCartItemGetAllDto>> Get(CancellationToken cancellationToken)
        => _mediator.Send(new ShoppingCartItemGetAllCommand(_httpContextAccessor.GetUserId()), cancellationToken);

    [HttpGet]
    public Task<ShoppingCartItemGetByIdDto> Get(long key, CancellationToken cancellationToken)
        => _mediator.Send(new ShoppingCartItemGetByIdCommand(key, _httpContextAccessor.GetUserId()), cancellationToken);

    [HttpPost]
    public Task Post([FromBody] ShoppingCartItemPostDto dto, CancellationToken cancellationToken)
        => _mediator.Send(new ShoppingCartItemPostCommand(_httpContextAccessor.GetUserId(), dto), cancellationToken);

    [HttpPut]
    public Task Put(long key, [FromBody] ShoppingCartItemPutDto dto, CancellationToken cancellationToken)
        => _mediator.Send(new ShoppingCartItemPutCommand(key, _httpContextAccessor.GetUserId(), dto), cancellationToken);
}
