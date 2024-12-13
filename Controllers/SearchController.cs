using Microsoft.AspNetCore.Mvc;
using Social_Life.Data;
using Social_Life.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Social_Life.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Search1()
        {
            return View("~/Views/Profile/Search.cshtml", new List<Profile>());
        }
        [HttpGet]
        public IActionResult Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                ViewBag.Message = "Introduceți un nume pentru a începe căutarea.";
                return View("~/Views/Profile/Search.cshtml", new List<Profile>());
            }
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var results = _context.Profiles
                .Where(p => (p.Username.Contains(query) || p.Nume.Contains(query) || p.Prenume.Contains(query))
                            && p.Id_User != currentUserId).OrderBy(p => p.Username).ToList();

            ViewBag.Query = query;
            return View("~/Views/Profile/Search.cshtml", results);
        }
    }
}