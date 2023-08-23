using FrameWorksExamen.Data;
using FrameWorksExamen.Models;
using Microsoft.AspNetCore.Mvc;
using FrameWorksExamen.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FrameWorksExamen.Controllers
{
    public class ApplicationController : Controller
    {
        protected readonly User _user;
        protected readonly ApplicationDbContext _context;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly ILogger<ApplicationController> _logger;
       
        public ApplicationController(ApplicationDbContext context,
                                        IHttpContextAccessor httpContextAccessor,
                                        ILogger<ApplicationController> logger)
        {
            
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            //_user = SessionUser.GetUser(httpContextAccessor.HttpContext, context);

            string? userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            if (userName == null)
                userName = "-";
            _user = _context.Users.FirstOrDefault(u => u.UserName == userName);

        }
    } 
}
