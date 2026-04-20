namespace BookSwapLite.Controllers
{
    using BookSwap.Data;
    using BookSwap.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using Microsoft.EntityFrameworkCore;
    using BookSwap.Core.ViewModels.Favorites;

[Authorize]
    public class FavoriteController : Controller
    {
        private readonly ApplicationDbContext context;

        public FavoriteController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int bookId)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            bool bookExists = await context.Books.AnyAsync(b => b.Id == bookId);
            if (!bookExists)
            {
                return NotFound();
            }

            bool exists = await context.Favorites
                .AnyAsync(f => f.BookId == bookId && f.UserId == userId);

            if (!exists)
            {
                var favorite = new Favorite
                {
                    BookId = bookId,
                    UserId = userId
                };

                await context.Favorites.AddAsync(favorite);
                await context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Book added to Favorites successfully.";
            }
            else
            {
                TempData["InfoMessage"] = "This book is already in your Favorites.";
            }

            return RedirectToAction("Index", "Book");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int bookId)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var favorite = await context.Favorites
                .FirstOrDefaultAsync(f => f.BookId == bookId && f.UserId == userId);

            if (favorite == null)
            {
                return NotFound();
            }

            context.Favorites.Remove(favorite);
            await context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Removed from Favorites.";

            return RedirectToAction(nameof(MyFavorites));
        }

        public async Task<IActionResult> MyFavorites()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var favorites = await context.Favorites
                .Include(f => f.Book)
                .Where(f => f.UserId == userId)
                .Select(f => new FavoriteViewModel
                {
                    Id = f.Book.Id,
                    Title = f.Book.Title,
                    Author = f.Book.Author
                })
                .ToListAsync();

            return View(favorites);
        }
    }
}