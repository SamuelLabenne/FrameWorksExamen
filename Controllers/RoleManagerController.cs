using FrameWorksExamen.Data;
using FrameWorksExamen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace FrameWorksExamen.Controllers
{
    public class RoleManagerController : ApplicationController
    {
        public RoleManagerController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<ApplicationController> logger)
            : base(context, httpContextAccessor, logger)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
