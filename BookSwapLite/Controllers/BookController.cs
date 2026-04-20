namespace BookSwapLite.Controllers
{
    using BookSwap.Core.Contracts;
    using BookSwap.Core.ViewModels.Books;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string? searchTerm, int page = 1)
        {
            int pageSize = 6;

            var result = await bookService.GetAllAsync(searchTerm, page, pageSize);

            ViewBag.SearchTerm = searchTerm;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(result.TotalCount / (double)pageSize);

            return View(result.Books);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new BookFormModel
            {
                Genres = await bookService.GetGenresAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = await bookService.GetGenresAsync();
                return View(model);
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            await bookService.CreateAsync(model, userId);

            TempData["SuccessMessage"] = "Book created successfully!";
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var book = await bookService.GetDetailsAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var model = await bookService.GetForEditAsync(id, userId);

            if (model == null)
            {
                return Forbid();
            }

            model.Genres = await bookService.GetGenresAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookFormModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                model.Genres = await bookService.GetGenresAsync();
                return View(model);
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            bool success = await bookService.UpdateAsync(id, model, userId);

            if (!success)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Book updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var model = await bookService.GetForEditAsync(id, userId);

            if (model == null)
            {
                return Forbid();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            bool success = await bookService.DeleteAsync(id, userId);

            if (!success)
            {
                TempData["ErrorMessage"] = "This book cannot be deleted because it has swap requests or you are not authorized.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Book deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}