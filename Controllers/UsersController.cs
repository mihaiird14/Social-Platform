using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Social_Life.Data;
using Social_Life.Models;
namespace Social_Life.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;
        public UsersController(ApplicationDbContext context,
                UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            db = context;
            _userManager = userManager;
            _env = env;
        }
        public IActionResult Index(string username)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                return NotFound();
            }

            var profile = db.Profiles.FirstOrDefault(p => p.Id_User == user.Id);
            if (profile == null)
            {
                return NotFound();
            }
            int NrThread = db.Threads.Count(p => p.Id_User == user.Id);
            ViewBag.NrThread = NrThread;

            return View(profile);
        }
        [HttpPost]
        public IActionResult NewCom_User(ThreadComment comentariu, string username)
        {
            try
            {
                var thread = db.Threads.FirstOrDefault(tl => tl.ThreadId == comentariu.ThreadId);
                thread.Profile = db.Profiles.FirstOrDefault(u => u.Id_User == thread.Id_User);
                if (thread == null)
                {
                    return NotFound("Thread-ul nu a fost găsit.");
                }
                var user = db.Profiles.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    return NotFound("Utilizatorul nu a fost găsit.");
                }
                var userId = user.Id_User;
                comentariu.Id_User = userId;
                comentariu.Date = DateTime.Now;
                thread.ThreadComments += 1;
                if (comentariu.CommentText == null)
                {
                    TempData["EditTh"] = "Comentariul este obligatoriu!";
                    return RedirectToAction("Index", "Users", new { username = thread.Profile.Username });
                }
                if (comentariu.CommentText.Length < 5 || comentariu.CommentText.Length > 100)
                {
                    TempData["EditTh"] = "Comentariul trebuie sa fie intre 5 si 100 caractere";
                    return RedirectToAction("Index", "Users", new { username = thread.Profile.Username });
                }
                TempData["EditTh"] = null;
                db.ThreadComments.Add(comentariu);
                db.SaveChanges();
                return RedirectToAction("Index", "Users", new { username = thread.Profile.Username });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction("Index", "Profile");
            }
        }
        [HttpPost]
        public IActionResult DeleteCom_User(int ThreadCommentId, string username)
        {

            var comentariu = db.ThreadComments.FirstOrDefault(t => t.ThreadCommentId == ThreadCommentId);
            var thread = db.Threads.FirstOrDefault(tl => tl.ThreadId == comentariu.ThreadId);
           
            if (comentariu == null)
            {
                return NotFound("Thread not found.");
            }
            thread.ThreadComments -= 1;
            db.ThreadComments.Remove(comentariu);
            db.SaveChanges();

            return RedirectToAction("Index", "Users", new { username = username});
        }
        [HttpPost]
        public IActionResult EditCom_User(ThreadComment comentariu,string username)
        {
            var exCom = db.ThreadComments.FirstOrDefault(t => t.ThreadCommentId == comentariu.ThreadCommentId);
            if (exCom == null)
            {
                return NotFound("Thread not found.");
            }
            if (comentariu.CommentText == null)
            {
                TempData["EditTh"] = "Comentariul trebuie sa fie intre 5 si 100 caractere";
                return RedirectToAction("Index", "Users", new { username = username });
            }
            if (comentariu.CommentText.Length < 5 || comentariu.CommentText.Length > 100)
            {
                TempData["EditTh"] = "Comentariul trebuie sa fie intre 5 si 100 caractere";
                return RedirectToAction("Index", "Users", new { username = username });
            }
            TempData["EditTh"] = null;
            exCom.Edited = true;
            exCom.CommentText = comentariu.CommentText;
            db.SaveChanges();

            return RedirectToAction("Index", "Users", new { username = username });
        }
    }
}
