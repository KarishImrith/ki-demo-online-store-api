using AutoMapper;
using OnlineStore.Database.Entities;
using OnlineStore.Logic.Concerns.ShoppingCartItemConcern.GetAll;
using OnlineStore.Logic.Concerns.ShoppingCartItemConcern.GetById;
using OnlineStore.Logic.Concerns.ShoppingCartItemConcern.Post;
using OnlineStore.Logic.Concerns.ShoppingCartItemConcern.Put;

namespace OnlineStore.Logic.Concerns.ShoppingCartItemConcern;

public class ShoppingCartItemProfile : Profile
{
    public ShoppingCartItemProfile()
    {
        this.CreateMap<ShoppingCartItem, ShoppingCartItemGetAllDto>()
            .ForMember(dest => dest.CurrentSellingPrice, opt => opt.MapFrom(src => src.Product.CurrentSellingPrice));

        this.CreateMap<ShoppingCartItem, ShoppingCartItemGetByIdDto>()
            .ForMember(dest => dest.CurrentSellingPrice, opt => opt.MapFrom(src => src.Product.CurrentSellingPrice));

        this.CreateMap<ShoppingCartItemPostDto, ShoppingCartItem>();
        this.CreateMap<ShoppingCartItemPutDto, ShoppingCartItem>();
    }
}
