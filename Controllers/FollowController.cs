using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social_Life.Data;
using Social_Life.Models;
using System.Linq;
using System.Security.Claims;

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


            var existingFollow = db.Follows
                .FirstOrDefault(f => f.Id_Urmaritor == currentUserId && f.Id_Urmarit == userToFollowId);

            if (existingFollow != null)
            {
                db.Follows.Remove(existingFollow);
                db.SaveChanges();
            }
            else
            {
                var follow = new Follow
                {
                    Id_Urmaritor = currentUserId,
                    Id_Urmarit = userToFollowId,
                    Data = DateTime.Now
                };
                db.Follows.Add(follow);
                db.SaveChanges();
            }

            db.SaveChanges();
            var username = db.Users
       .Where(u => u.Id == userToFollowId)
       .Select(u => u.UserName)
       .FirstOrDefault();

            if (string.IsNullOrEmpty(username))
            {
                return NotFound("Utilizatorul nu a fost găsit.");
            }

            return RedirectToAction("Index", "Users", new { username = username });


        }
    }
}
