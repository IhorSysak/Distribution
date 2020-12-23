using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Distributor.DAL.Entities;

namespace Distributor.DAL.EF
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Student> students { get; set; }
        public ICollection<Task> tasks { get; set; }
        public ApplicationUser()
        {
            this.students = new Collection<Student>();
            this.tasks = new Collection<Task>();
        }
        public async System.Threading.Tasks.Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {


            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}