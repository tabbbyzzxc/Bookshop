using Bookshop.ProductsLib;
using BookshopWeb.Models;
using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace BookshopWeb.Controllers
{
    public class BookController : Controller
    {
        private readonly ProductDbContext _context;

        public BookController(ProductDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBook(BookViewModel bookModel)
        {
            var book = new Book
            {
                UniqueId = Guid.NewGuid(),
                Title = bookModel.Title,
                BuyPrice = bookModel.BuyPrice,
                Author = bookModel.Author,
                Description = bookModel.Description,
                Genre = bookModel.Genre,
                Language = bookModel.Language,
                PageQuantity = bookModel.PageQuantity,
                PaperType = bookModel.PaperType,
                SellPrice = bookModel.SellPrice,
            };
            
            _context.Books.Add(book);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }



        
    }
}
