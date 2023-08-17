using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FrameWorksExamen.Models;

namespace FrameWorksExamen.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<FrameWorksExamen.Models.Event> Event { get; set; } = default!;
        public DbSet<FrameWorksExamen.Models.Invite> Invite { get; set; } = default!;
        public DbSet<FrameWorksExamen.Models.Person> Person { get; set; } = default!;
    }
}