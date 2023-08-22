using FrameWorksExamen.Data;
using Microsoft.AspNetCore.Mvc;

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
