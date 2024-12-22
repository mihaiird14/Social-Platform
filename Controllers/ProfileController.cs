using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Social_Life.Data;
using Social_Life.Models;
using System;
using System.Net.NetworkInformation;
using System.Security.Claims;
namespace Social_Life.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;
        public ProfileController(ApplicationDbContext context,
                UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            db = context;
            _userManager= userManager;
            _env = env;
        }
         public IActionResult Index()
         {
            var userId = _userManager.GetUserId(User);
            int NrThread = db.Threads.Count(p => p.Id_User == userId);
            ViewBag.NrThread = NrThread;
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }

            var pr = db.Profiles.FirstOrDefault(p => p.Id_User == userId);          
            return View();
         }
        public IActionResult Edit()
        {
            var userId = _userManager.GetUserId(User);
            var pr = db.Profiles.FirstOrDefault(p => p.Id_User == userId);
            if (pr == null)
            {
                ViewBag.PozaProfil = "/imagini/pozaDefaultProfil.jpg";
            }
            else
            {
                ViewBag.PozaProfil = pr.ProfileImage;
                ViewBag.bio = pr.Bio;
            }
            
            return View();
        }
        [HttpPost]
        public IActionResult New(Thread2 thread)
        {
            try
            {
                if (thread.ThreadText == null)
                {
                    TempData["ErrorMessage"] = "Textul este obligatoriu!";
                    return RedirectToAction("Index", "Profile");
                }
                var userId = _userManager.GetUserId(User);
                thread.Id_User = userId;
                thread.Date = DateTime.Now;
                if(thread.ThreadText.Length<5 || thread.ThreadText.Length > 100)
                {
                    TempData["ErrorMessage"] = "Textul trebuie sa fie intre 5 si 100 caractere";
                    return RedirectToAction("Index", "Profile");
                }
                db.Threads.Add(thread);
                db.SaveChanges();
                TempData["ErrorMessage"] = null;
                return RedirectToAction("Index");
                
            }

            catch
            {
                TempData["ErrorMessage"] = "Textul trebuie sa fie intre 5 si 100 caractere";
                return RedirectToAction("Index","Profile");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(int id, Profile profile, IFormFile ProfileImage)
        {

            var userId = _userManager.GetUserId(User);
            var pr = db.Profiles.FirstOrDefault(p => p.Id_User == userId);
            if (pr == null)
            {
                return NotFound("Profilul nu a fost găsit.");
            }

            try
            {
                if (ProfileImage != null && ProfileImage.Length > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var fileExtension = Path.GetExtension(ProfileImage.FileName).ToLower();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        return View();
                    }
                    // Cale stocare
                    var storagePath = Path.Combine(_env.WebRootPath, "imagini", ProfileImage.FileName);
                    var databaseFileName = "/imagini/" + ProfileImage.FileName;
                    using (var fileStream = new FileStream(storagePath, FileMode.Create))
                    {
                        await ProfileImage.CopyToAsync(fileStream);
                    }
                    ModelState.Remove(nameof(pr.ProfileImage));
                    pr.ProfileImage = databaseFileName;
                }
                if (profile.Bio == null)
                {
                    TempData["EroareEdit"] = "Descrierea este obligatorie!";

                    return RedirectToAction("Edit", "Profile");
                }
                if (profile.Bio.Length < 3 || profile.Bio.Length > 50) {
         
                    TempData["EroareEdit"] = "Descrierea trebuie sa aiba intre 3-50 caractere";
                    
                    return RedirectToAction("Edit", "Profile");
                }
                pr.Bio = profile.Bio;
                TempData["EroareEdit"] = null;
                db.SaveChanges();
                return RedirectToAction("Index", "Profile");
            }
            catch (Exception e)
            {
                
                return RedirectToAction("Edit", "Profile");
            }
        }
        [HttpPost]
        public IActionResult DefaultProfile(int id)
        {

            var userId = _userManager.GetUserId(User);
            var pr = db.Profiles.FirstOrDefault(p => p.Id_User == userId);
            if (pr == null)
            {
                return NotFound("Profilul nu a fost găsit.");
            }

            try
            {
                pr.ProfileImage = "/imagini/pozaDefaultProfil.jpg";
                db.SaveChanges();
                return RedirectToAction("Edit", "Profile");
            }
            catch (Exception e)
            {

                return RedirectToAction("Edit", "Profile");
            }
        }
        [HttpPost]
        public async Task<IActionResult> ToggleFollow(string userToFollowId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null || userToFollowId == null)
                return RedirectToAction("Index", "Home");

            var existingFollow = db.Follows
                .FirstOrDefault(f => f.Id_Urmaritor == currentUser.Id && f.Id_Urmarit == userToFollowId);

            if (existingFollow == null)
            {

                var newFollow = new Follow
                {
                    Id_Urmaritor = currentUser.Id,
                    Id_Urmarit = userToFollowId,
                    Data = DateTime.Now
                };
                db.Follows.Add(newFollow);
            }
            else
            {
                db.Follows.Remove(existingFollow);
            }

            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Profile", new { id = userToFollowId });
        }
        [HttpGet]
        public IActionResult SearchFollow(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                TempData["ListaF"] = null;
                return RedirectToAction("Index", "Profile");
            }
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var results = db.Profiles
     .Where(p => db.Follows
         .Any(f => f.Id_Urmaritor == currentUserId && f.Id_Urmarit == p.Id_User) &&
                 (p.Username.Contains(query) || p.Nume.Contains(query) || p.Prenume.Contains(query)) &&
                 p.Id_User != currentUserId)
     .OrderBy(p => p.Username)
     .ToList();


            ViewBag.Query = query;
            TempData["ListaF"] = JsonConvert.SerializeObject(results);
            return RedirectToAction("Index", "Profile");
        }
        [HttpPost]
        public IActionResult DeleteFollower(string idUrmaritor, string idUrmarit)
        {

            var f = db.Follows.FirstOrDefault(t => t.Id_Urmaritor == idUrmaritor && t.Id_Urmarit==idUrmarit);
            Console.WriteLine(f);
            if (f == null)
            {
                return NotFound();
            }
            db.Follows.Remove(f);
            db.SaveChanges();

            return RedirectToAction("Index", "Profile");
        }
        [HttpGet]
        public IActionResult SearchFollowing(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                TempData["ListaFF"] = null;
                return RedirectToAction("Index", "Profile");
            }
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var results = db.Profiles
     .Where(p => db.Follows
         .Any(f => f.Id_Urmarit == currentUserId && f.Id_Urmaritor == p.Id_User) &&
                 (p.Username.Contains(query) || p.Nume.Contains(query) || p.Prenume.Contains(query)) &&
                 p.Id_User != currentUserId)
     .OrderBy(p => p.Username)
     .ToList();


            ViewBag.Query = query;
            TempData["ListaFF"] = JsonConvert.SerializeObject(results);
            return RedirectToAction("Index", "Profile");
        }
        [HttpPost]
        public IActionResult DeleteFollower2(string idUrmaritor, string idUrmarit)
        {

            var f = db.Follows.FirstOrDefault(t => t.Id_Urmarit == idUrmarit && t.Id_Urmaritor == idUrmaritor);
            
            if (f == null)
            {
                Console.WriteLine("ewewewewewew");
                return NotFound();
            }
            db.Follows.Remove(f);
            db.SaveChanges();
 
            return RedirectToAction("Index", "Profile");
        }
    }
}
