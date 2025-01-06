using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Social_Life.Data;
<<<<<<< HEAD
using Social_Life.Migrations;
=======
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
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
<<<<<<< HEAD

=======
       
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
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
<<<<<<< HEAD
            if (thread.ThreadText == null)
=======
            if(thread.ThreadText == null)
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
            {
                TempData["EditTh"] = "Thread-ul este obligatoriu!";
                return RedirectToAction("Index", "Profile");
            }
<<<<<<< HEAD
            if (thread.ThreadText.Length < 5 || thread.ThreadText.Length > 400)
=======
            if(thread.ThreadText.Length<5 || thread.ThreadText.Length > 400)
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
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
<<<<<<< HEAD
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
=======
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc

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
<<<<<<< HEAD
                return Json(new { success = false });
=======
                return Json(new { success = false});
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
            }
            var newLike = new ThreadLike
            {
                ThreadId = threadId,
                ProfileId = userId,
                LikeDate = DateTime.UtcNow
            };
<<<<<<< HEAD
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
=======

>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
            db.ThreadLikes.Add(newLike);
            thread.ThreadLikes += 1;

            db.SaveChanges();
<<<<<<< HEAD
            return Json(new { success = true, liked = true, likes = thread.ThreadLikes });
=======
            return Json(new { success = true, liked= true, likes = thread.ThreadLikes });
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
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
<<<<<<< HEAD
                if (comentariu.CommentText.Length < 5 || comentariu.CommentText.Length > 100)
=======
                if(comentariu.CommentText.Length<5 || comentariu.CommentText.Length > 100)
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
                {
                    TempData["EditTh"] = "Comentariul trebuie sa fie intre 5 si 100 caractere";
                    return RedirectToAction("Index", "Profile");
                }
<<<<<<< HEAD
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
=======
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
                TempData["EditTh"] = null;
                db.ThreadComments.Add(comentariu);
                db.SaveChanges();
                return RedirectToAction("Index", "Profile");
            }
<<<<<<< HEAD
            catch (Exception e)
=======
            catch(Exception e)
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
            {
                return RedirectToAction("Index", "Profile");
            }
        }
        [HttpPost]
        public IActionResult DeleteCom(int ThreadCommentId)
        {
<<<<<<< HEAD

=======
            
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
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
<<<<<<< HEAD
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
=======

>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
            db.ThreadCommentsLikes.Add(newLike);
            comentariu.Likes += 1;

            db.SaveChanges();
            return Json(new { success = true, liked = true, likes = comentariu.Likes });
        }
    }
}