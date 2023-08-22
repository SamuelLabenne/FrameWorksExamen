using FrameWorksExamen.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Utilities;

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
                User user2 = null;
                
               if(!context.Roles.Any()) {  
                    context.Roles.AddRange(
                          new IdentityRole { Id = "User", Name = "User", NormalizedName = "user" },
                          new IdentityRole { Id = "SystemAdministrator", Name = "SystemAdmninistrator", NormalizedName = "systemadministrator" });
                context.SaveChanges();} 
               


                if (!context.Users.Any())
                {
                   
                    user = new User
                    {
                        Id="1",
                        FirstName = "System",
                        LastName = "Administrator",
                        UserName = "Admin",
                        Email = "System.Administrator@app.com",
                        EmailConfirmed = true
                    };
                    user2 = new User
                    {
                        FirstName = "Normal",
                        LastName = "User",
                        UserName = "User",
                        Email = "normal.user@app.com",
                        EmailConfirmed = true
                    };
                    userManager.CreateAsync(user, "Abcd!1234");
                    userManager.CreateAsync(user2, "Abcd!1234");


                      
                }
                

                if (user != null && user2!=null)
                {
                    context.UserRoles.AddRange(
                           new IdentityUserRole<string> { UserId = user.Id, RoleId = "SystemAdministrator" },
                           new IdentityUserRole<string> { UserId = user2.Id, RoleId = "User" }
                           );
                }
                if (!context.Event.Any())
                {
                    context.Event.AddRange
                            (
                                     new Event { Name = "Festival", Description = "feestje in boom", date = DateTime.Today, Location = "Brussel", deleted = false },
                                     new Event { Name = "BBQ", Description = "very fun", date = DateTime.Today, Location = "Antwerpen", deleted = false }

                            );
                    context.SaveChanges();
                    if (!context.Invite.Any())
                {
                    context.Invite.AddRange
                            (
                                    new Invite { PersonId= 1, EventId=1, SenderId="1", deleted = false },
                                    new Invite { PersonId = 2, EventId = 1, SenderId = "1", deleted = false }

                            );
                    context.SaveChanges();
                }

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
