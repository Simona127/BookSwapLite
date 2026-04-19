using BookSwap.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
[Authorize(Roles = "Administrator")]
public class AdminUserController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;

    public AdminUserController(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    public IActionResult Index()
    {
        var users = userManager.Users.ToList();
        return View(users);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MakeAdmin(string id)
    {
        var user = await userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        await userManager.AddToRoleAsync(user, "Administrator");

        TempData["SuccessMessage"] = "User promoted to Admin!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveAdmin(string id)
    {
        var user = await userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        await userManager.RemoveFromRoleAsync(user, "Administrator");

        TempData["SuccessMessage"] = "Admin rights removed!";
        return RedirectToAction(nameof(Index));
    }
}