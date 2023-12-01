using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.RateLimiting;
using OnlineStore.Logic.Concerns.ProductConcern.Delete;
using OnlineStore.Logic.Concerns.ProductConcern.GetAll;
using OnlineStore.Logic.Concerns.ProductConcern.GetById;
using OnlineStore.Logic.Concerns.ProductConcern.Post;
using OnlineStore.Logic.Concerns.ProductConcern.Put;

namespace OnlineStore.App.Controllers;

[Authorize]
[EnableRateLimiting(nameof(OnlineStore))]
public class ProductController : ODataController
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete]
    public Task Delete(long key, CancellationToken cancellationToken)
        => _mediator.Send(new ProductDeleteCommand(key), cancellationToken);

    [HttpGet]
    [EnableQuery]
    public Task<IQueryable<ProductGetAllDto>> Get(CancellationToken cancellationToken)
        => _mediator.Send(new ProductGetAllCommand(), cancellationToken);

    [HttpGet]
    public Task<ProductGetByIdDto> Get(long key, CancellationToken cancellationToken)
        => _mediator.Send(new ProductGetByIdCommand(key), cancellationToken);

    [HttpPost]
    public Task Post([FromBody] ProductPostDto dto, CancellationToken cancellationToken)
        => _mediator.Send(new ProductPostCommand(dto), cancellationToken);

    [HttpPut]
    public Task Put(long key, [FromBody] ProductPutDto dto, CancellationToken cancellationToken)
        => _mediator.Send(new ProductPutCommand(key, dto), cancellationToken);
}
