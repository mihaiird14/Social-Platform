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
            var notifications = db.Notifications.Where(p => p.Id_User == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList();
            foreach (var n in notifications)
            {
                n.IsRead = true;
            }
            db.SaveChanges();
            return View();
        }

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
                var follow = new Follow
                {
                    Id_Urmaritor = requesterId,
                    Id_Urmarit = targetId,
                    Data = DateTime.Now
                };
                db.Follows.Add(follow);

                var feedbackNotification = new Notification
                {
                    Id_User = requesterId,
                    Id_User2 = targetId,
                    NotificationType = "FollowAccepted",
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
                db.SaveChanges();
            }
            else
            {
                var feedbackNotification = new Notification
                {
                    Id_User = requesterId,
                    Id_User2 = targetId,
                    NotificationType = "FollowRejected",
                    Message = $"{db.Profiles.FirstOrDefault(p => p.Id_User == targetId)?.Username} ți-a respins cererea de urmărire.",
                    Date = DateTime.Now
                };

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
