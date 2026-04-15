using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookSwapLite.Areas.Admin.Controllers
{
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

        public async Task<IActionResult> MakeAdmin(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await userManager.AddToRoleAsync(user, "Administrator");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveAdmin(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await userManager.RemoveFromRoleAsync(user, "Administrator");

            return RedirectToAction(nameof(Index));
        }
    }
}