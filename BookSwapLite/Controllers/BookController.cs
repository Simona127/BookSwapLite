namespace BookSwapLite.Controllers
{
    using BookSwap.Services.Books;
    using BookSwap.Web.ViewModels.Books;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var books = await bookService.GetAllBooksAsync();
            return View(books);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new BookFormModel();

            model.Genres = await bookService.GetGenresAsync();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = await bookService.GetGenresAsync();
                return View(model);
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await bookService.CreateAsync(model, userId);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var book = await bookService.GetDetailsAsync(id);

            return View(book);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await bookService.GetForEditAsync(id);

            model.Genres = await bookService.GetGenresAsync();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = await bookService.GetGenresAsync();

                return View(model);
            }
            await bookService.UpdateAsync(id, model);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await bookService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
