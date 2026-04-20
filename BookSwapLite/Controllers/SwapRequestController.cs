namespace BookSwapLite.Controllers
{
    using BookSwap.Core.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

[Authorize]
    public class SwapRequestController : Controller
    {
        private readonly ISwapRequestService swapRequestService;

        public SwapRequestController(ISwapRequestService swapRequestService)
        {
            this.swapRequestService = swapRequestService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int bookId)
        {
            if (bookId <= 0)
            {
                return BadRequest();
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            try
            {
                await swapRequestService.CreateRequestAsync(bookId, userId);
                TempData["SuccessMessage"] = "Swap request sent successfully!";
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Index", "Book");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
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

            try
            {
                await swapRequestService.ApproveAsync(id, userId);
                TempData["SuccessMessage"] = "Swap request approved.";
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(MyRequests));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id)
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

            try
            {
                await swapRequestService.RejectAsync(id, userId);
                TempData["SuccessMessage"] = "Swap request rejected.";
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(MyRequests));
        }

        [HttpGet]
        public async Task<IActionResult> MyRequests()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var myRequests = await swapRequestService.GetRequestsForOwnerAsync(userId);

            return View(myRequests);
        }
    }
}