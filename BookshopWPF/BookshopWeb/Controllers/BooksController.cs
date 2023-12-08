using AutoMapper;
using Bookshop.ProductsLib;
using BookshopWeb.Models;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookshopWeb.Controllers
{
    public class BooksController : Controller
    {
        private readonly ProductDbContext _context;
        private readonly IMapper _mapper;

        public BooksController(ProductDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Edit list
        public async Task<IActionResult> EditSelector()
        {
            var allBooks = await _context.Books.AsNoTracking().OrderBy(x => x.Id).ToListAsync();
            var models = _mapper.Map<List<BookViewModel>>(allBooks);
            EditBookListViewModel model = new() { Books = models };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetBookInfo([FromRoute] long id)
        {
            var book = await _context.Books.FirstAsync(x => x.Id == id);
            var model = _mapper.Map<ViewBookViewModel>(book);

            return View(model);
        }

        [HttpPost]
        public ActionResult AddBook(BookViewModel bookModel)
        {
            /*var book = new Book
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
            };*/

            var book = _mapper.Map<Book>(bookModel);

            _context.Books.Add(book);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> EditBook([FromRoute] long id)
        {
            var book = await _context.Books.FindAsync(id);
            var model = _mapper.Map<EditBookViewModel>(book);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(long id, EditBookViewModel model)
        {
            var book = await _context.Books.FindAsync(id);
            _mapper.Map(model, book);

            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return RedirectToAction("EditSelector");
        }

        /*public async Task<IActionResult> BookList()
        {
            var allBooks = await _context.Books.AsNoTracking().OrderBy(x => x.Id).ToListAsync();
            var cart = new Cart();
            await _context.Carts.AddAsync(cart);
            _context.SaveChanges();
            var models = _mapper.Map<List<BookViewModel>>(allBooks);
            NewOrderViewModel model = new NewOrderViewModel { Books = models, CartId = cart.Id };

            return PartialView(model);
        }*/
    }
}
