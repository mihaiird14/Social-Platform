using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Social_Life.Data;
using Social_Life.Models;
using System.Security.Claims;

namespace Social_Life.Controllers
{
    public class NotificationController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        public NotificationController(ApplicationDbContext context,
                UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            db = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
<<<<<<< HEAD
            var notifications = db.Notifications.Where(p => p.Id_User == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList();
            foreach (var n in notifications)
            {
                n.IsRead = true;
            }
            db.SaveChanges();
            return View();
        }

=======
            return View();
        }
        // [HttpPost]
        // public IActionResult RequestFollow(string userToFollowId)
        // {
        //     var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //     // Verificăm dacă utilizatorul există
        //     var profileToFollow = db.Profiles.FirstOrDefault(p => p.Id_User == userToFollowId);

        //     if (profileToFollow == null)
        //     {
        //         return NotFound("Utilizatorul nu a fost găsit.");
        //     }

        //     // Verificăm dacă utilizatorul este deja urmărit
        //     var existingFollow = db.Follows.FirstOrDefault(f => f.Id_Urmaritor == currentUserId && f.Id_Urmarit == userToFollowId);

        //     if (existingFollow != null)
        //     {
        //         return BadRequest("Deja urmăriți acest utilizator.");
        //     }

        //     if (!profileToFollow.ProfilPublic)
        //     {
        //         // Creăm o notificare pentru utilizatorul care trebuie să accepte/refuze
        //         var notification = new Notification
        //         {
        //             Id_User = userToFollowId, // Proprietarul profilului
        //             Id_User2 = currentUserId, // Utilizatorul care a trimis cererea
        //             NotificationType = "FollowRequest",
        //             Message = "Ai o cerere de urmărire de la un utilizator.",
        //             Date = DateTime.Now,
        //             Accepted = false // Implicit, nu este acceptată
        //         };

        //         db.Notifications.Add(notification);
        //         db.SaveChanges();

        //         return Ok("Cererea de urmărire a fost trimisă.");
        //     }
        //     else
        //     {
        //         // Adăugăm direct relația de urmărire
        //         var follow = new Follow
        //         {
        //             Id_Urmaritor = currentUserId,
        //             Id_Urmarit = userToFollowId,
        //             Data = DateTime.Now
        //         };

        //         db.Follows.Add(follow);
        //         db.SaveChanges();

        //         return Ok("Acum urmăriți acest utilizator.");
        //     }
        //     db.SaveChanges();
        //     var username = db.Users
        //.Where(u => u.Id == userToFollowId)
        //.Select(u => u.UserName)
        //.FirstOrDefault();

        //     if (string.IsNullOrEmpty(username))
        //     {
        //         return NotFound("Utilizatorul nu a fost găsit.");
        //     }

        //     return RedirectToAction("Index", "Users", new { username = username });

        // }
        //[HttpPost]
        //public IActionResult HandleFollowRequest(int notificationId, bool accept)
        //{
        //    var notification = db.Notifications.FirstOrDefault(n => n.NotificationId == notificationId);
        //    if (notification == null)
        //    {
        //        return NotFound("Notificarea nu a fost găsită.");
        //    }

        //    if (accept)
        //    {
        //        // Adăugăm relația de urmărire
        //        var follow = new Follow
        //        {
        //            Id_Urmaritor = notification.Id_User2,
        //            Id_Urmarit = notification.Id_User,
        //            Data = DateTime.Now
        //        };
        //        db.Follows.Add(follow);
        //    }



        //    db.Notifications.Remove(notification);
        //    db.SaveChanges();

        //    TempData["Message"] = accept ? "Ai acceptat cererea de urmărire." : "Ai respins cererea de urmărire.";
        //    return RedirectToAction("Index", "Notification");
        //}
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
        [HttpPost]
        public IActionResult HandleFollowRequest(int notificationId, bool accept)
        {
            var notification = db.Notifications.FirstOrDefault(n => n.NotificationId == notificationId);
            if (notification == null)
            {
                return NotFound("Notificarea nu a fost găsită.");
            }

            var requesterId = notification.Id_User2;
            var targetId = notification.Id_User;

            if (accept)
            {
<<<<<<< HEAD
=======
                // Adăugăm relația de urmărire
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
                var follow = new Follow
                {
                    Id_Urmaritor = requesterId,
                    Id_Urmarit = targetId,
                    Data = DateTime.Now
                };
                db.Follows.Add(follow);

<<<<<<< HEAD
=======
                // Notificăm utilizatorul care a trimis cererea
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
                var feedbackNotification = new Notification
                {
                    Id_User = requesterId,
                    Id_User2 = targetId,
                    NotificationType = "FollowAccepted",
<<<<<<< HEAD
                    Message = $"{db.Profiles.FirstOrDefault(p => p.Id_User == targetId)?.Username} ți-a acceptat cererea de urmărire.",
                    Date = DateTime.Now
                };
                db.Notifications.Add(feedbackNotification);
                //ptr celalat
                var feedbackNotification2 = new Notification
                {
                    Id_User = targetId,
                    Id_User2 = requesterId,
                    NotificationType = "FollowAccepted2",
                    Message = $"I-ai acceptat cererea de urmărire lui {db.Profiles.FirstOrDefault(p => p.Id_User == requesterId)?.Username}.",
                    Date = DateTime.Now
                };
                db.Notifications.Add(feedbackNotification2);
=======
                    Message = $"Utilizatorul {db.Profiles.FirstOrDefault(p => p.Id_User == targetId)?.Username} ți-a acceptat cererea de urmărire.",
                    Date = DateTime.Now
                };
                db.Notifications.Add(feedbackNotification);
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
                db.SaveChanges();
            }
            else
            {
<<<<<<< HEAD
=======
                // Notificăm utilizatorul că cererea a fost respinsă
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
                var feedbackNotification = new Notification
                {
                    Id_User = requesterId,
                    Id_User2 = targetId,
                    NotificationType = "FollowRejected",
<<<<<<< HEAD
                    Message = $"{db.Profiles.FirstOrDefault(p => p.Id_User == targetId)?.Username} ți-a respins cererea de urmărire.",
                    Date = DateTime.Now
                };

=======
                    Message = $"Utilizatorul {db.Profiles.FirstOrDefault(p => p.Id_User == targetId)?.Username} ți-a respins cererea de urmărire.",
                    Date = DateTime.Now
                };
                
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
                db.Notifications.Add(feedbackNotification);
                db.SaveChanges();
            }

            // Ștergem notificarea inițială
            db.Notifications.Remove(notification);
            db.SaveChanges();

            TempData["Message"] = accept ? "Ai acceptat cererea de urmărire." : "Ai respins cererea de urmărire.";
            return RedirectToAction("Index", "Notification");
        }
    }
}
