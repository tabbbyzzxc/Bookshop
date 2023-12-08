using AutoMapper;
using Bookshop.ProductsLib;
using BookshopWeb.Models;

namespace BookshopWeb.Mappings
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<BookViewModel, Book>();
            CreateMap<Book, BookViewModel>();
            CreateMap<Book, OrderBookListViewModel>();
            CreateMap<Book, EditBookViewModel>();
            CreateMap<EditBookViewModel, Book>().ForMember(dest => dest.Id, act => act.Ignore());
            CreateMap<Book, ViewBookViewModel>();
        }
    }
}
