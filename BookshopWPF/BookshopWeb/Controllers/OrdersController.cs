using AutoMapper;
using Bookshop.ProductsLib;
using BookshopWeb.Models;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookshopWeb.Controllers
{
    public class OrdersController : Controller
    {

        private readonly ProductDbContext _context;
        private readonly IMapper _mapper;

        public OrdersController(ProductDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var allBooks = await _context.Books.AsNoTracking().OrderBy(x => x.Id).ToListAsync();
            var cart = new Cart();
            await _context.Carts.AddAsync(cart);
            _context.SaveChanges();
            var models = _mapper.Map<List<OrderBookListViewModel>>(allBooks);
            models.ForEach(x =>
            {
                x.CartId = cart.Id;
            });

            NewOrderViewModel model = new() { Books = models };

            return View(model);
        }
    }
}
