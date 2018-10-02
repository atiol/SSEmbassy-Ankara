using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ssembassy_ankara.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string ImgUrl { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Position { get; set; }

        [AllowHtml]
        public string Biography { get; set; }

        [AllowHtml]
        public string Message { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<@event> Event { get; set; }
        public DbSet<event_category> EventCategory { get; set; }
        public DbSet<positions> Positions { get; set; }
        public DbSet<embassy_address> EmbassyAddress { get; set; }
        public DbSet<article> Articles { get; set; }
        public DbSet<article_category> ArticleCategory { get; set; }
        public DbSet<VisaInfo> VisaInfo { get; set; }
        public DbSet<ImportantNotice> ImportantNotice { get; set; }
        public DbSet<messages> Messages { get; set; }
        public DbSet<CitizenRegistration> CitizenRegistration { get; set; }
        public DbSet<MaritalStatus> MaritalStatus { get; set; }
        public DbSet<ModeOfTransport> ModeOfTransport { get; set; }
        public DbSet<OnlineVisaApplication> OnlineVisaApplication { get; set; }
        public DbSet<PassportType> PassportType { get; set; }
        public DbSet<PurposeOfVisit> PurposeOfVisit { get; set; }
        public DbSet<Sex> Sex { get; set; }
        public DbSet<VisaTypeRequested> VisaTypeRequested { get; set; }
        public DbSet<BusinessInvestments> BusinessInvestments { get; set; }
        public DbSet<SSHistory> SSHistory { get; set; }
        public DbSet<TurkeyRelations> TurkeyRelations { get; set; }
        public DbSet<EmbassyMission> EmbassyMission { get; set; }
        public DbSet<EducationAndCulture> EducationAndCulture { get; set; }
        public DbSet<VisaApproval> VisaApproval { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<OnlineVisaApplication>()
                .HasRequired<Sex>(s => s.Sex)
                .WithMany(g => g.OnlineVisaApplication)
                .HasForeignKey<int>(s => s.GenderId);

            modelBuilder.Entity<OnlineVisaApplication>()
                .HasRequired<Sex>(s => s.Sex2)
                .WithMany(g => g.OnlineVisaApplication2)
                .HasForeignKey<int?>(s => s.ReferenceGenderId);

            base.OnModelCreating(modelBuilder);
        }
        
    }
}