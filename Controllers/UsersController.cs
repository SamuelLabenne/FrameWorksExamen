 using FrameWorksExamen.Data;
using FrameWorksExamen.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrameWorksExamen.Controllers
{
    public class UsersController : ApplicationController
    {
        public UsersController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<ApplicationController> logger)
            : base(context, httpContextAccessor, logger)
        {

        }
        public IActionResult Index(string userName, string firstName, string lastName, string email)
        {
            List<UserViewModel> vmUsers = new List<UserViewModel>();
            List<User> users = _context.Users
                                                .Where(u => u.UserName != "Dummy"
                                                        && (u.UserName.Contains(userName) || string.IsNullOrEmpty(userName))
                                                        && (u.FirstName.Contains(firstName) || string.IsNullOrEmpty(firstName))
                                                        && (u.LastName.Contains(lastName) || string.IsNullOrEmpty(lastName))
                                                        && (u.Email.Contains(email) || string.IsNullOrEmpty(email)))
                                                .ToList();
            foreach (User user in users)
            {
                vmUsers.Add(new UserViewModel
                {
                    
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Roles = (from userRole in _context.UserRoles
                             where userRole.UserId == user.Id
                             orderby userRole.RoleId
                             select userRole.RoleId).ToList()
                });
            }
            ViewData["userName"] = userName;
            ViewData["firstName"] = firstName;
            ViewData["lastName"] = lastName;
            ViewData["email"] = email;
           
            return View(vmUsers);
        }

        public IActionResult Undelete(string userName)
        {
            User user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            
            _context.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string userName)
        {
            User user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            
            _context.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Roles(string userName)
        {

            User user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            RoleViewModel rvm = new RoleViewModel()
            {
                UserName = userName,
                Roles = (from userRole in _context.UserRoles
                         where userRole.UserId == user.Id
                         orderby userRole.RoleId
                         select userRole.RoleId).ToList()
            };

            ViewData["RoleIds"] = new MultiSelectList(_context.Roles.OrderBy(c => c.Name), "Id", "Name", rvm.Roles);
            return View(rvm);
        }

        [HttpPost]
        public IActionResult Roles([Bind("UserName,Roles")] RoleViewModel _model)
        {
            User user = _context.Users.FirstOrDefault(u => u.UserName == _model.UserName);
            List<IdentityUserRole<string>> userRoles = _context.UserRoles.Where(ur => ur.UserId == user.Id).ToList();
            foreach (IdentityUserRole<string> ur in userRoles)
            {
                _context.Remove(ur);
            }
            if (_model.Roles != null)
                foreach (string roleId in _model.Roles)
                    _context.UserRoles.Add(new IdentityUserRole<string>() { RoleId = roleId, UserId = user.Id });
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
