using FrameWorksExamen.Data;
using FrameWorksExamen.Models;
using FrameWorksExamen.Models.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FrameWorksExamen.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<User> _signinManager;

        public AccountController(ApplicationDbContext context, SignInManager<User> signInManager)
        {
            _context = context;
            _signinManager = signInManager;
        }
        [HttpPost]
        [Route("/API/Login")]
        [Route("Login")]
        public async Task<ActionResult<Boolean>> Login([FromBody] LoginModel @login)
        {
            var result = await _signinManager.PasswordSignInAsync(@login.UserName, @login.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}
