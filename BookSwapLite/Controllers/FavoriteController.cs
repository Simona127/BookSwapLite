namespace BookSwapLite.Controllers
{
    using BookSwap.Data;
    using BookSwap.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using Microsoft.EntityFrameworkCore;

    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly ApplicationDbContext context;

        public FavoriteController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Add(int bookId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

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

        public async Task<IActionResult> MyFavorites()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var favorites = await context.Favorites
                .Include(f => f.Book)
                .Where(f => f.UserId == userId)
                .Select(f => new
                {
                    f.Book.Id,
                    f.Book.Title,
                    f.Book.Author
                })
                .ToListAsync();

            return View(favorites);
        }
    }
}