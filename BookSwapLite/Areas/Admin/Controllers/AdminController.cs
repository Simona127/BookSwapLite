namespace BookSwapLite.Areas.Admin.Controllers
{
    using BookSwap.Core.Contracts;
    using BookSwap.Core.ViewModels.Admin;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IBookService bookService;

        public AdminController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new AdminDashboardViewModel
            {
                TotalBooks = await bookService.GetBooksCountAsync(),
                TotalUsers = await bookService.GetUsersCountAsync(),
                TotalRequests = await bookService.GetRequestsCountAsync()
            };

            return View(model);
        }
    }
}