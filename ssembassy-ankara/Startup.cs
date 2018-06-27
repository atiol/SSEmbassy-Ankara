using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ssembassy_ankara.Startup))]
namespace ssembassy_ankara
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
