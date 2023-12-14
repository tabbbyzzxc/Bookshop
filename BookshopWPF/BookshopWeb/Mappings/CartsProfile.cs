using AutoMapper;
using Bookshop.ProductsLib;
using BookshopWeb.Models;

namespace BookshopWeb.Mappings
{
    public class CartsProfile : Profile
    {
        public CartsProfile()
        {
            CreateMap<CartLine, OrderLine>().ForMember(dest => dest.Id, act => act.Ignore());
            CreateMap<CartLine, CartLineViewModel>();
        }
    }
}
