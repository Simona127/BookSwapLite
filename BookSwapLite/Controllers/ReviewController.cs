using BookSwapLite.Services.Reviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookSwapLite.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;
        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }
        public IActionResult Add(string userId)
        {
            ViewBag.UserId = userId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(string userId, int rating, string? comment)
        {
            string reviewerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await reviewService.AddReviewAsync(reviewerId!, userId, rating, comment);
            return RedirectToAction("Profile", "User", new { id = userId });
        }
    }
}
