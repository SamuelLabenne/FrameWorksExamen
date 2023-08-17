using FrameWorksExamen.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FrameWorksExamen.Data
{
    public class SeedDatacontext
    {
        public static void Initialize(IServiceProvider serviceProvider, UserManager<User> userManager)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService
                                                              <DbContextOptions<ApplicationDbContext>>()))
            {
                context.Database.EnsureCreated();

                User user = null;

                if (!context.Users.Any())
                {
                    user = new User
                    {
                        FirstName = "System",
                        LastName = "Administrator",
                        UserName = "Admin",
                        Email = "System.Administrator@GroupSpace.be",
                        EmailConfirmed = true
                    };
                    userManager.CreateAsync(user, "Abc!12345");

                    context.Roles.AddRange(
                           new IdentityRole { Id = "User", Name = "User", NormalizedName = "user" },
                           new IdentityRole { Id = "SystemAdministrator", Name = "SystemAdmninistrator", NormalizedName = "systemadministrator" });
                    context.SaveChanges();
                }
                if (user != null)
                {
                    context.UserRoles.AddRange(
                           new IdentityUserRole<string> { UserId = user.Id, RoleId = "SystemAdministrator" });
                }
                if (!context.Event.Any())
                {
                    context.Event.AddRange
                            (
                                     new Event { Name = "?", Description = "?", date = DateTime.Today, Location = "Brussel", people = "who", deleted = false },
                                     new Event { Name = "Hello", Description = "very fun", date = DateTime.Today, Location = "nergens", people = "everyone", deleted = false }

                            );
                    context.SaveChanges();
                }
                
            }
        }
    }
}
