using FrameWorksExamen.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Utilities;

namespace FrameWorksExamen.Data
{
    public class SeedDatacontext
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider, UserManager<User> userManager)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService
                                                              <DbContextOptions<ApplicationDbContext>>()))
            {
                context.Database.EnsureCreated();
                context.Database.Migrate();

               if(!context.Roles.Any()) {
                    User user = new User
                    {
                        
                        FirstName = "System",
                        LastName = "Administrator",
                        UserName = "Admin",
                        Email = "System.Administrator@app.com",
                        EmailConfirmed = true
                    };
                    User user2 = new User
                    {
                        
                        FirstName = "Normal",
                        LastName = "User",
                        UserName = "User",
                        Email = "normal.user@app.com",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(user, "Abcd!1234");
                    await userManager.CreateAsync(user2, "Abcd!1234");
                    

                    context.Roles.AddRange(
                          new IdentityRole { Id = "User", Name = "User", NormalizedName = "user" },
                          new IdentityRole { Id = "SystemAdministrator", Name = "SystemAdmninistrator", NormalizedName = "systemadministrator" });
                    context.SaveChanges();

                    
                    
                    context.UserRoles.AddRange(
                           new IdentityUserRole<string> { UserId = user.Id, RoleId = "SystemAdministrator" },
                           new IdentityUserRole<string> { UserId = user2.Id, RoleId = "User" }
                           );
                    context.SaveChanges();
                } 
               User normalUser = context.Users.FirstOrDefault(u => u.UserName == "User");
               User administrator = context.Users.FirstOrDefault(u => u.UserName == "Admin");

                if (!context.Event.Any())
                {
                    context.Event.AddRange
                            (
                                     new Event { Name = "Festival", Description = "feestje in boom", date = DateTime.Today, Location = "Brussel", deleted = false },
                                     new Event { Name = "BBQ", Description = "very fun", date = DateTime.Today, Location = "Antwerpen", deleted = false }

                            );
                    context.SaveChanges();

                    

                }
                
                if (!context.Person.Any())
                {
                    context.Person.AddRange
                            (
                                    new Person { Name = "Simon", Age = 25, deleted = false },
                                    new Person { Name = "Greet", Age = 44, deleted = false }

                            );
                    context.SaveChanges();
                }
                
            } 
        }
    }
}
