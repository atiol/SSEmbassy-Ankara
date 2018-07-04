using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ssembassy_ankara.Models;

[assembly: OwinStartupAttribute(typeof(ssembassy_ankara.Startup))]
namespace ssembassy_ankara
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        // create default User roles and Admin User for login
        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // create default admin user and role
            if (!roleManager.RoleExists("Admin"))
            {
                // create admin role first
                var role = new IdentityRole { Name = "Admin" };
                roleManager.Create(role);


                // create admin user
                var user = new ApplicationUser
                {
                    UserName = "Atiol",
                    Email = "atl.rafa@gmail.com"
                };

                string uPassword = "H@pili1992";

                var chkUser = userManager.Create(user, uPassword);

                // Add default user to Admin role
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Admin");
                }
            }

            if (!roleManager.RoleExists("Staff"))
            {
                var role = new IdentityRole { Name = "Staff" };
                roleManager.Create(role);
            }
        }
    }
}
