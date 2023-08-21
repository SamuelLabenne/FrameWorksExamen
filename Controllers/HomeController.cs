using FrameWorksExamen.Data;
using FrameWorksExamen.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FrameWorksExamen.Controllers
{
    public class HomeController : ApplicationController
    {
        

        public HomeController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<ApplicationController> logger)
            : base(context, httpContextAccessor, logger)
        {

        }

        public IActionResult Index()
        {
            User user = _user;
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