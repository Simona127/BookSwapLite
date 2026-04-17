using BookSwap.Core.Contracts;
using BookSwap.Core.ViewModels.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookSwapLite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class AdminBookController : Controller
    {
        private readonly IBookService bookService;

        public AdminBookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await bookService.GetAllBooksAsync();
            return View(books);
        }

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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var model = await bookService.GetForEditAsync(id, userId);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookFormModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await bookService.UpdateAsync(id, model, userId);

            if (!result)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Book updated successfully!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await bookService.GetDetailsAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var result = await bookService.DeleteAsync(id, userId);

            if (!result)
            {
                TempData["SuccessMessage"] = "Delete failed!";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Book deleted successfully!";

            return RedirectToAction(nameof(Index));
        }
    }
}