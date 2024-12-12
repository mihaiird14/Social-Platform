using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
            
            var pr = db.Profiles.FirstOrDefault(p => p.Id_User == userId);
            if (pr == null)
            {
                ViewBag.PozaProfil = "/imagini/pozaDefaultProfil.jpg";
            }
            else
            {
                ViewBag.PozaProfil = pr.ProfileImage;
            }
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
                var userId = _userManager.GetUserId(User);
                thread.Id_User = userId;
                thread.Date = DateTime.Now;
                db.Threads.Add(thread);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            catch (Exception)
            {
                Console.WriteLine(thread.ThreadText);
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
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png"};
                    var fileExtension = Path.GetExtension(ProfileImage.FileName).ToLower();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        return View();
                    }
                    // Cale stocare
                    var storagePath = Path.Combine(_env.WebRootPath, "imagini",ProfileImage.FileName);
                    var databaseFileName = "/imagini/" + ProfileImage.FileName;
                    using (var fileStream = new FileStream(storagePath, FileMode.Create))
                    {
                        await ProfileImage.CopyToAsync(fileStream);
                    }
                    ModelState.Remove(nameof(pr.ProfileImage));
                    pr.ProfileImage = databaseFileName;
                }
                pr.Bio = profile.Bio;
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
    }
}
