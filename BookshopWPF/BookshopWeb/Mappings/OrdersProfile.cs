using AutoMapper;
using Bookshop.ProductsLib;
using BookshopWeb.Models;

namespace BookshopWeb.Mappings
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderViewModel, Order>();
            CreateMap<OrderLine, CartLine>().ForMember(dest => dest.Id, act => act.Ignore());
        }
    }
}
