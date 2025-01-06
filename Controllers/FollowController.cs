using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social_Life.Data;
using Social_Life.Models;
using System.Linq;
using System.Security.Claims;
<<<<<<< HEAD
using System.Threading;
=======
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc

namespace Social_Life.Controllers
{
    [Authorize]
    public class FollowController : Controller
    {
        private readonly ApplicationDbContext db;

        public FollowController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpPost]
        public IActionResult ToggleFollow(string userToFollowId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var profileToFollow = db.Profiles.FirstOrDefault(p => p.Id_User == userToFollowId);
            if (profileToFollow == null)
            {
                return NotFound("Profilul nu a fost găsit.");
            }

            var existingFollow = db.Follows
                .FirstOrDefault(f => f.Id_Urmaritor == currentUserId && f.Id_Urmarit == userToFollowId);

            if (existingFollow != null)
            {
<<<<<<< HEAD
                foreach (var notificare in db.Notifications.Where(p => p.Id_User == userToFollowId && p.Id_User2 == currentUserId))
                {
                    db.Notifications.Remove(notificare);
                }
=======
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
                db.Follows.Remove(existingFollow);
                db.SaveChanges();
            }
            else
            {
                if (!profileToFollow.ProfilPublic)
                {
<<<<<<< HEAD

                    var notification = new Notification
                    {
                        Id_User = userToFollowId,
                        Id_User2 = currentUserId,
=======
                    
                    var notification = new Notification
                    {
                        Id_User = userToFollowId, 
                        Id_User2 = currentUserId, 
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
                        NotificationType = "FollowRequest12",
                        Message = "Ai o cerere de urmărire de la un utilizator.",
                        Date = DateTime.Now,
                        Accepted = false
                    };

                    db.Notifications.Add(notification);
                    db.SaveChanges();

                    TempData["Message"] = "Cererea de urmărire a fost trimisă.";
                    return RedirectToAction("Index", "Users", new { username = profileToFollow.Username });
                }
                else
                {
                    var follow = new Follow
                    {
                        Id_Urmaritor = currentUserId,
                        Id_Urmarit = userToFollowId,
                        Data = DateTime.Now
                    };

<<<<<<< HEAD

                    var notificare = new Notification
                    {
                        Id_User = userToFollowId,
                        Id_User2 = currentUserId,
                        NotificationType = "FollowAccepted",
                        Message = $"{User.Identity.Name} a început să te urmărească!",
                        Date = DateTime.Now
                    };
                    db.Notifications.Add(notificare);
=======
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
                    db.Follows.Add(follow);
                    db.SaveChanges();
                }
            }

            TempData["Message"] = "Acțiunea a fost finalizată.";
            return RedirectToAction("Index", "Users", new { username = profileToFollow.Username });
        }

    }
}
