 using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MVC_WEB_Page.Anotations;

namespace MVC_WEB_Page.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        /*Modifications start */
           //Add user birthdate
          [Required]
          public DateTime BirthDate { get; set; }
          [Required]
          [StringLength(15, ErrorMessage = "The name must be at least 3 characters long.", MinimumLength = 3)]
          public String Name{ get; set; }
          [Required]
          [StringLength(15, ErrorMessage = "The surname must be at least 3 characters long.", MinimumLength = 3)]
          public String Surname { get; set; }
          [GenderValidator(ErrorMessage="Wrong gender")]
          public int  Gender{get;set;}
          public String Image { get; set; }
          
        //<-- modifications end
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<UsersGalleries> Galleries { get; set; }
        public DbSet<Announcments> Announcments { get; set; }
        public DbSet<Comments> Comments { get; set; }
    }//<-- 
    
}//<-- namespace end