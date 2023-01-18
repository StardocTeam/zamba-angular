using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Zamba.Web.Startup))]
namespace Zamba.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
