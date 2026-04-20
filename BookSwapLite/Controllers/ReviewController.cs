using BookSwap.Core.Contracts;
using BookSwap.Core.ViewModels.Reviews;
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
            var model = new ReviewFormModel
            {
                UserId = userId
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ReviewFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string reviewerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            await reviewService.AddReviewAsync(reviewerId, model.UserId, model.Rating, model.Comment);

            TempData["SuccessMessage"] = "Review added successfully.";

            return RedirectToAction("Profile", "User", new { id = model.UserId });
        }
        [AllowAnonymous]
        public async Task<IActionResult> ForUser(string userId)
        {
            var reviews = await reviewService.GetReviewsForUserAsync(userId);
            var averageRating = await reviewService.GetAverageRatingAsync(userId);

            ViewBag.AverageRating = averageRating;
            ViewBag.UserId = userId;

            return View(reviews);
        }
    }
}
