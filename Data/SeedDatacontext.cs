using FrameWorksExamen.Models;
using Microsoft.EntityFrameworkCore;

namespace FrameWorksExamen.Data
{
    public class SeedDatacontext
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService
                                                              <DbContextOptions<ApplicationDbContext>>()))
            {
                context.Database.EnsureCreated();


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
