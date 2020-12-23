using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Distributor.DAL.Entities;

namespace Distributor.DAL.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<ControlCenter> ControlCenters { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}