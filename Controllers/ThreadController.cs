using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Social_Life.Data;
using Social_Life.Migrations;
using Social_Life.Models;
using System;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Threading;

namespace Social_Life.Controllers
{
    public class ThreadController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        public ThreadController(ApplicationDbContext context,
                UserManager<ApplicationUser> userManager)
        {
            db = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int threadId)
        {

            var thread = db.Threads.FirstOrDefault(t => t.ThreadId == threadId);

            if (thread == null)
            {
                return NotFound("Thread not found.");
            }

            db.Threads.Remove(thread);
            db.SaveChanges();

            return RedirectToAction("Index", "Profile");
        }
        [HttpPost]
        public IActionResult EditThread(Thread2 thread)
        {
            var existingThread = db.Threads.FirstOrDefault(t => t.ThreadId == thread.ThreadId);
            if (existingThread == null)
            {
                return NotFound("Thread not found.");
            }
            existingThread.Edited = true;
            existingThread.ThreadText = thread.ThreadText;
            if (thread.ThreadText == null)
            {
                TempData["EditTh"] = "Thread-ul este obligatoriu!";
                return RedirectToAction("Index", "Profile");
            }
            if (thread.ThreadText.Length < 5 || thread.ThreadText.Length > 400)
            {
                TempData["EditTh"] = "Thread-ul trebuie sa fie intre 5 si 400 caractere";
                return RedirectToAction("Index", "Profile");
            }
            TempData["EditTh"] = null;
            db.SaveChanges();

            return RedirectToAction("Index", "Profile");
        }
        [HttpPost]
        public IActionResult ToggleLike(int threadId)
        {
            var thread = db.Threads.FirstOrDefault(p => p.ThreadId == threadId);
            if (thread == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }
            var userLike = db.ThreadLikes.FirstOrDefault(tl => tl.ThreadId == threadId && tl.ProfileId == userId);


            if (userLike != null)
            {
                thread.ThreadLikes -= 1;
                db.ThreadLikes.Remove(userLike);
                db.SaveChanges();
                return Json(new { success = false });
            }
            var newLike = new ThreadLike
            {
                ThreadId = threadId,
                ProfileId = userId,
                LikeDate = DateTime.UtcNow
            };
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (thread.Id_User != currentUserId)
            {
                var notificare = new Notification
                {
                    Id_User = thread.Id_User, // Proprietarul postării
                    Id_User2 = currentUserId, // Cel care dă like
                    NotificationType = "Like",
                    Message = $"{User.Identity.Name} ți-a apreciat thread-ul.",
                    Date = DateTime.Now
                };
                db.Notifications.Add(notificare);
            }
            db.ThreadLikes.Add(newLike);
            thread.ThreadLikes += 1;

            db.SaveChanges();
            return Json(new { success = true, liked = true, likes = thread.ThreadLikes });
        }
        [HttpPost]
        public IActionResult NewCom(ThreadComment comentariu)
        {
            try
            {
                if (comentariu.CommentText == null)
                {
                    TempData["EditTh"] = "Comentariul este obligatoriu!";
                    return RedirectToAction("Index", "Profile");
                }
                var thread = db.Threads.FirstOrDefault(tl => tl.ThreadId == comentariu.ThreadId);
                var userId = _userManager.GetUserId(User);
                comentariu.Id_User = userId;
                comentariu.Date = DateTime.Now;
                thread.ThreadComments += 1;
                if (comentariu.CommentText.Length < 5 || comentariu.CommentText.Length > 100)
                {
                    TempData["EditTh"] = "Comentariul trebuie sa fie intre 5 si 100 caractere";
                    return RedirectToAction("Index", "Profile");
                }
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (comentariu.Id_User != currentUserId)
                {
                    var notificare = new Notification
                    {
                        Id_User = comentariu.Id_User, // Proprietarul postării
                        Id_User2 = currentUserId, // Cel care dă like
                        NotificationType = "Like",
                        Message = $"{User.Identity.Name} a lăsat un comentariu la un thread pe care l-ai postat!",
                        Date = DateTime.Now
                    };
                    db.Notifications.Add(notificare);
                }
                TempData["EditTh"] = null;
                db.ThreadComments.Add(comentariu);
                db.SaveChanges();
                return RedirectToAction("Index", "Profile");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Profile");
            }
        }
        [HttpPost]
        public IActionResult DeleteCom(int ThreadCommentId)
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

            return RedirectToAction("Index", "Profile");
        }
        [HttpPost]
        public IActionResult EditCom(ThreadComment comentariu)
        {
            var exCom = db.ThreadComments.FirstOrDefault(t => t.ThreadCommentId == comentariu.ThreadCommentId);
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
        public IActionResult ToggleLikeComment(int ThreadCommentId)
        {
            var comentariu = db.ThreadComments.FirstOrDefault(p => p.ThreadCommentId == ThreadCommentId);
            if (comentariu == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }
            var userLike = db.ThreadCommentsLikes.FirstOrDefault(tl => tl.Comment_Id == ThreadCommentId && tl.User_id == userId);


            if (userLike != null)
            {
                comentariu.Likes -= 1;
                db.ThreadCommentsLikes.Remove(userLike);
                db.SaveChanges();
                return Json(new { success = false });
            }
            var newLike = new ThreadCommentsLike
            {
                Comment_Id = ThreadCommentId,
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
                    Message = $"{User.Identity.Name} ți-a apreciat un comentariu la un thread!",
                    Date = DateTime.Now
                };
                db.Notifications.Add(notificare);
            }
            db.ThreadCommentsLikes.Add(newLike);
            comentariu.Likes += 1;

            db.SaveChanges();
            return Json(new { success = true, liked = true, likes = comentariu.Likes });
        }
    }
}