using Microsoft.AspNetCore.Mvc;
using Social_Life.Data;
using Social_Life.Models;
using System.Diagnostics;

namespace Social_Life.Controllers
{

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;
        public HomeController(ApplicationDbContext context)
        {
            db = context;
        }
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
