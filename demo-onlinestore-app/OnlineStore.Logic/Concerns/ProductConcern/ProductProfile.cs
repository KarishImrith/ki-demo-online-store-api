using AutoMapper;
using OnlineStore.Database.Entities;
using OnlineStore.Logic.Concerns.ProductConcern.GetAll;
using OnlineStore.Logic.Concerns.ProductConcern.GetById;
using OnlineStore.Logic.Concerns.ProductConcern.Post;
using OnlineStore.Logic.Concerns.ProductConcern.Put;

namespace OnlineStore.Logic.Concerns.ProductConcern;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        this.CreateMap<Product, ProductGetAllDto>();
        this.CreateMap<Product, ProductGetByIdDto>();
        this.CreateMap<ProductPostDto, Product>();
        this.CreateMap<ProductPutDto, Product>();
    }
}
