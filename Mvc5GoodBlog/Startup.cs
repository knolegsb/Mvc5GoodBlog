using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mvc5GoodBlog.Startup))]
namespace Mvc5GoodBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
