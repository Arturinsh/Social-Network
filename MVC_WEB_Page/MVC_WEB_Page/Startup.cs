using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_WEB_Page.Startup))]
namespace MVC_WEB_Page
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
