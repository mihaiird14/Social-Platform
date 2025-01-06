using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Life.Data;
using Social_Life.Migrations;
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
                if (descriere.Length < 5 || descriere.Length > 50)
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
            var userLike = db.PostareLikes2.FirstOrDefault(tl => tl.PostareId == postareId && tl.ProfileId == userId);


            if (userLike != null)
            {
                postare.NrLikePostare -= 1;
                db.PostareLikes2.Remove(userLike);
                db.SaveChanges();
                return Json(new { success = false });
            }
            var newLike = new PostareLike2
            {
                PostareId = postareId,
                ProfileId = userId,
                LikeDate = DateTime.UtcNow
            };
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (postare.UserId != currentUserId)
            {
                var notificare = new Notification
                {
                    Id_User = postare.UserId, // Proprietarul postării
                    Id_User2 = currentUserId, // Cel care dă like
                    NotificationType = "Like",
                    Message = $"{User.Identity.Name} ți-a apreciat postarea.",
                    Date = DateTime.Now
                };
                db.Notifications.Add(notificare);
            }
            db.PostareLikes2.Add(newLike);
            postare.NrLikePostare += 1;

            db.SaveChanges();
            return Json(new { success = true, liked = true, likes = postare.NrLikePostare });
        }
        [HttpPost]
        public IActionResult NewCom(PostsComment comentariu)
        {
            try
            {
                if (comentariu.CommentText == null)
                {
                    TempData["EditTh"] = "Comentariul este obligatoriu!";
                    return RedirectToAction("Index", "Profile");
                }
                var postare = db.Postari.FirstOrDefault(tl => tl.Id == comentariu.PostId);
                var userId = _userManager.GetUserId(User);
                comentariu.Id_User = userId;
                comentariu.Date = DateTime.Now;
                if (comentariu.CommentText.Length < 5 || comentariu.CommentText.Length > 100)
                {
                    TempData["EditTh"] = "Comentariul trebuie sa fie intre 5 si 100 caractere";
                    return RedirectToAction("Index", "Profile");
                }

                postare.NrComentarii += 1;
                TempData["EditTh"] = null;
                db.PostsComments.Add(comentariu);
                db.SaveChanges();
                return RedirectToAction("Index", "Profile");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Profile");
            }
        }
        [HttpPost]
        public IActionResult DeleteCom(int PostCommentId)
        {

            var comentariu = db.PostsComments.FirstOrDefault(t => t.PostCommentId == PostCommentId);
            var post = db.Postari.FirstOrDefault(tl => tl.Id == comentariu.PostId);
            if (comentariu == null)
            {
                return NotFound("Thread not found.");
            }
            post.NrComentarii -= 1;
            db.PostsComments.Remove(comentariu);
            db.SaveChanges();

            return RedirectToAction("Index", "Profile");
        }
        [HttpPost]
        public IActionResult EditCom(PostsComment comentariu)
        {
            var exCom = db.PostsComments.FirstOrDefault(t => t.PostCommentId == comentariu.PostCommentId);
            if (exCom == null)
            {
                return NotFound("Thread not found.");
            }
            if (comentariu.CommentText == null)
            {
                TempData["EditTh"] = "Comentariul este obligatoriu!";
                return RedirectToAction("Index", "Profile");
            }
            if (comentariu.CommentText.Length < 5 || comentariu.CommentText.Length > 100)
            {
                TempData["EditTh"] = "Comentariul trebuie sa fie intre 5 si 100 caractere";
                return RedirectToAction("Index", "Profile");
            }
            TempData["EditTh"] = null;
            exCom.Edited = true;
            exCom.CommentText = comentariu.CommentText;
            db.SaveChanges();

            return RedirectToAction("Index", "Profile");
        }
        [HttpPost]
        public IActionResult ToggleLikeComment(int PostCommentId)
        {
            var comentariu = db.PostsComments.FirstOrDefault(p => p.PostCommentId == PostCommentId);
            if (comentariu == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }
            var userLike = db.PostCommentsLikes.FirstOrDefault(tl => tl.Comment_Id == PostCommentId && tl.User_id == userId);


            if (userLike != null)
            {
                comentariu.Likes -= 1;
                db.PostCommentsLikes.Remove(userLike);
                db.SaveChanges();
                return Json(new { success = false });
            }
            var newLike = new PostCommentsLike
            {
                Comment_Id = PostCommentId,
                User_id = userId,
                Data = DateTime.UtcNow
            };
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId != comentariu.Id_User)
            {
                var notificare = new Notification
                {
                    Id_User = comentariu.Id_User, // Proprietarul postării
                    Id_User2 = currentUserId, // Cel care dă like
                    NotificationType = "Like",
                    Message = $"{User.Identity.Name} a apreciat un comentariu la o postare!",
                    Date = DateTime.Now
                };
                db.Notifications.Add(notificare);
            }
            db.PostCommentsLikes.Add(newLike);
            comentariu.Likes += 1;

            db.SaveChanges();
            return Json(new { success = true, liked = true, likes = comentariu.Likes });
        }
    }
}
