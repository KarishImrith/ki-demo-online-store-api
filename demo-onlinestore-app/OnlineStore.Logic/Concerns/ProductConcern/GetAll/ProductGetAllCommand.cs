using MediatR;

namespace OnlineStore.Logic.Concerns.ProductConcern.GetAll;

public class ProductGetAllCommand : IRequest<IQueryable<ProductGetAllDto>>
{
}
