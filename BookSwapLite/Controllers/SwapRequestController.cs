namespace BookSwapLite.Controllers
{
    using BookSwapLite.Services.SwapRequests;
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
        public async Task<IActionResult> CreateRequest(int bookId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await swapRequestService.CreateRequestAsync(bookId, userId);
            return RedirectToAction("Index", "Book");
        }
        [HttpPost]
        public async Task<IActionResult> Approve(int requestId)
        {
            await swapRequestService.ApproveAsync(requestId);

            return RedirectToAction("Index", "Book");
        }
        [HttpPost]
        public async Task<IActionResult> Reject(int requestId)
        {
            await swapRequestService.RejectAsync(requestId);

            return RedirectToAction("Index", "Book");
        }
        [Authorize]
        public async Task<IActionResult> MyRequests()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var myRequests = await swapRequestService.GetRequestsForOwnerAsync(userId);

            return View(myRequests);
        }
    }
}
