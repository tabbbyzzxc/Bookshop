using AutoMapper;
using Bookshop.ProductsLib;
using BookshopWeb.Models;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookshopWeb.Controllers
{
    [Authorize]
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

        public async Task<IActionResult> ViewOrders()
        {
            var allOrders = await _context.Orders
                .AsNoTracking()
                .Include(x => x.OrderList)
                    .ThenInclude(o => o.Book)
                .ToListAsync();
            var models = _mapper.Map<List<OrderViewModel>>(allOrders);

            OrderListViewModel model = new() { Orders = models };

            return View(model);
        }
    }
}
