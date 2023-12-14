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
    public class CartsController : Controller
    {
        private readonly ProductDbContext _context;
        private readonly IMapper _mapper;

        public CartsController(ProductDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var userCart = await _context.Carts
                .AsNoTracking()
                .Include(x => x.CartLines)
                .ThenInclude(x => x.Book)
                .FirstAsync(x => x.AspNetUserId == GetUserId());
            var models = _mapper.Map<List<CartLineViewModel>>(userCart.CartLines);
            CartListViewModel model = new() { CartLines = models };
            return View(model);
        }

        [HttpPost]
        public async Task<IsSuccess> Update(OnChangeModel model)
        {
            if (model.BookId == 0)
            {
                return (new IsSuccess(false, "Book does not exist"));
            }

            if (int.Parse(model.Quantity) <= 0)
            {
                //Delete(model.bookId)
            }
            var userCart = await _context.Carts
                .Include(x => x.CartLines)
                .FirstAsync(x => x.AspNetUserId == GetUserId());
            userCart.CartLines.First(x => x.Id == model.BookId).Quantity = int.Parse(model.Quantity);
            _context.Update(userCart);
            _context.SaveChanges();

            return (new IsSuccess(true, "Quantity updated"));
        }

        [HttpPost]
        public async Task<IsSuccess> AddToCart(AddToCartModel model)
        {
            var cart = await _context.Carts
                .Include(x => x.CartLines)
                .FirstOrDefaultAsync(x => x.AspNetUserId == GetUserId());
            if (cart == null)
            {
                return (new IsSuccess(false, "Cart does not exist"));
            }

            var bookId = await _context.Books
                .AsNoTracking()
                .Where(x => x.Id == model.BookId)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            if (bookId == 0)
            {
                return (new IsSuccess(false, "Book does not exist"));
            }

            if (cart.CartLines.Any(x => x.BookId == bookId))
            {
                var existingCartLine = cart.CartLines.First(x => x.BookId == bookId).Quantity++;
                await _context.SaveChangesAsync();
                return (new IsSuccess(true, "Quantity updated"));
            }

            var cartLine = new CartLine()
            {
                CartId = cart.Id,
                BookId = bookId,
                Quantity = 1
            };
            cart.CartLines.Add(cartLine);
            await _context.SaveChangesAsync();
            return (new IsSuccess(true, "Book added"));
        }

        public async Task<IsSuccess> Delete(RemoveModel model)
        {
            var cart = await _context.Carts
                .Include(x => x.CartLines)
                .FirstOrDefaultAsync(x => x.AspNetUserId == GetUserId());
            if (cart == null)
            {
                return new IsSuccess(false, "Cart does not exist");
            }

            var cartLine = cart.CartLines.Find(x => x.Id == model.Id);
            if (cartLine == null)
            {
                return new IsSuccess(false, "Cartline does not exist");
            }

            cart.CartLines.Remove(cartLine);
            await _context.SaveChangesAsync();

            return new IsSuccess(true, "Cartline removed");
        }

        private string GetUserId()
        {
            return _context.Users.First(x => x.UserName == User.Identity.Name).Id;
        }
    }

    public record AddToCartModel(long BookId);
    public record OnChangeModel(long BookId, string Quantity);
    public record RemoveModel(long Id);

    public record IsSuccess(bool Response, string message);
}
