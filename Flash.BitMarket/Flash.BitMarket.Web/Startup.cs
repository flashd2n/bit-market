using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Flash.BitMarket.Web.Startup))]
namespace Flash.BitMarket.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
