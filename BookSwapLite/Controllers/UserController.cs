using BookSwap.Services.Books;
using BookSwapLite.Services.Reviews;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

    public async Task<IActionResult> Profile(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var books = await bookService.GetAllBooksAsync();
        var userBooks = books.Where(b => b.OwnerId == id);

        var rating = await reviewService.GetAverageRatingAsync(id);

        ViewBag.UserName = user.UserName;
        ViewBag.Rating = rating;
        ViewBag.UserId = id;

        return View(userBooks);
    }
}