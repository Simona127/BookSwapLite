namespace BookSwapLite.Controllers
{
    using BookSwap.Services.Books;
    using BookSwap.Web.ViewModels.Books;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    public class BookController : Controller
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }
        public async Task<IActionResult> Index()
        {
            var books = await bookService.GetAllAsync();
            return View(books);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await bookService.CreateAsync(model, userId);
            return RedirectToAction(nameof(Index));
        }
    }
}
