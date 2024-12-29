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
                if(descriere.Length<5 || descriere.Length > 50)
                {
                    TempData["EditTh"] = "Descrierea trebuie sa fie intre 5 si 50 caractere";
                    return RedirectToAction("Index", "Profile");
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
            else
            {
                TempData["EditTh"] = "Poza este obligatorie!";
            }
            return RedirectToAction("Index", "Profile");
        }
        [HttpPost]
        public IActionResult Delete(int postareId)
        {

            var postare = db.Postari.FirstOrDefault(t => t.Id == postareId);

            if (postare == null)
            {
                return NotFound("Thread not found.");
            }

            db.Postari.Remove(postare);
            db.SaveChanges();

            return RedirectToAction("Index", "Profile");
        }
        [HttpPost]
        public IActionResult EditPostare(Postare postare)
        {
            var existingPostare = db.Postari.FirstOrDefault(t => t.Id == postare.Id);
            if (existingPostare == null)
            {
                return NotFound("Thread not found.");
            }
            existingPostare.Descriere = postare.Descriere;
            if (postare.Descriere == null)
            {
                TempData["EditTh"] = "Descrierea este obligatorie!";
                return RedirectToAction("Index", "Profile");
            }
            if (postare.Descriere.Length < 5 || postare.Descriere.Length > 50)
            {
                TempData["EditTh"] = "Descrierea trebuie sa fie intre 5 si 50 caractere";
                return RedirectToAction("Index", "Profile");
            }
            TempData["EditTh"] = null;
            db.SaveChanges();

            return RedirectToAction("Index", "Profile");
        }
        [HttpPost]
        public IActionResult LikePostare(int postareId)
        {
            var postare = db.Postari.FirstOrDefault(p => p.Id == postareId);
            if (postare == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }
            var userLike = db.PostareLikes.FirstOrDefault(tl => tl.PostareId == postareId && tl.ProfileId == userId);


            if (userLike != null)
            {
                postare.NrLikePostare -= 1;
                db.PostareLikes.Remove(userLike);
                db.SaveChanges();
                return Json(new { success = false });
            }
            var newLike = new PostareLike
            {
                PostareId = postareId,
                ProfileId = userId,
                LikeDate = DateTime.UtcNow
            };

            db.PostareLikes.Add(newLike);
            postare.NrLikePostare += 1;

            db.SaveChanges();
            return Json(new { success = true, liked = true, likes = postare.NrLikePostare });
        }
    }
}
