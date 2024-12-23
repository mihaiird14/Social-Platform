using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Life.Data;
using Social_Life.Models;
using System.Security.Claims;

namespace Social_Life.Controllers
{
    public class SetariController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        public SetariController(ApplicationDbContext context,
                UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            db = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SetarePublic()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = await db.Profiles.FirstOrDefaultAsync(p => p.Id_User == userId);

            if (profile == null)
            {
                return NotFound();
            }

            profile.ProfilPublic = !profile.ProfilPublic;

            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Setari");
        }
    }
}

