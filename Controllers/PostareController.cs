using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Life.Data;
using Social_Life.Models;
using System.Linq;
using System.Security.Claims;

namespace Social_Life.Controllers
{
    public class PostareController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;
        public PostareController(ApplicationDbContext context,
                UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            db = context;
            _userManager = userManager;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> NewPostare(IFormFile imagine, string descriere)
        {
            if (imagine != null && imagine.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var fileExtension = Path.GetExtension(imagine.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return View();
                }
                // Cale stocare
                var storagePath = Path.Combine(_env.WebRootPath, "postari", imagine.FileName);
                var databaseFileName = "/postari/" + imagine.FileName;
                using (var fileStream = new FileStream(storagePath, FileMode.Create))
                {
                    await imagine.CopyToAsync(fileStream);
                }
                var postare = new Postare
                {
                    UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value, // Obține UserId-ul curent
                    Imagine = databaseFileName, // Calea relativă a imaginii
                    Descriere = descriere,
                    Data = DateTime.Now
                };
                db.Postari.Add(postare);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Profile");
            }
            return RedirectToAction("Index", "Profile");
        }
    }
}
