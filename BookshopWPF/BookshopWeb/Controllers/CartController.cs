using Bookshop.ProductsLib;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookshopWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductDbContext _context;

        public CartController(ProductDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IsSuccess> AddToCart(AddToCartModel model)
        {
            var cart = await _context.Carts
                .Include(x => x.CartLines)
                .FirstOrDefaultAsync(x => x.Id == model.cartId);
            if (cart == null)
            {
                return (new IsSuccess(false));
            }

            var bookId = await _context.Books
                .AsNoTracking()
                .Where(x => x.Id == model.BookId).Select(x => x.Id).FirstOrDefaultAsync();

            if (bookId == 0)
            {
                return (new IsSuccess(false));
            }

            if (cart.CartLines.Any(x => x.BookId == bookId))
            {
                var existingCartLine = cart.CartLines.First(x => x.BookId == bookId).Quantity++;
                await _context.SaveChangesAsync();
                return (new IsSuccess(true));
            }
            var cartLine = new CartLine()
            {
                CartId = cart.Id,
                BookId = bookId,
                Quantity = 1
            };
            cart.CartLines.Add(cartLine);
            await _context.SaveChangesAsync();
            return (new IsSuccess(true));
        }
    }

    public record AddToCartModel(long cartId, long BookId);

    public record IsSuccess(bool response);
}
