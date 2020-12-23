using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Distributor.WEB.Startup))]
namespace Distributor.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
