namespace BookSwapLite.Controllers
{
    using BookSwap.Core.Contracts;
    using BookSwap.Core.ViewModels.Users;
    using BookSwap.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IBookService bookService;
        private readonly IReviewService reviewService;

    public UserController(
        UserManager<ApplicationUser> userManager,
        IBookService bookService,
        IReviewService reviewService)
        {
            this.userManager = userManager;
            this.bookService = bookService;
            this.reviewService = reviewService;
        }

        [AllowAnonymous] 
        public async Task<IActionResult> Profile(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userBooks = (await bookService.GetAllBooksAsync())
                .Where(b => b.OwnerId == id)
                .ToList();

            var rating = await reviewService.GetAverageRatingAsync(id);

            var model = new UserProfileViewModel
            {
                UserId = id,
                UserName = user.UserName!,
                Rating = rating,
                Books = userBooks
            };

            return View(model);
        }
    }
}