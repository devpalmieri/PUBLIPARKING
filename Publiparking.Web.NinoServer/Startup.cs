using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Publiparking.Web.NinoServer.Startup))]
namespace Publiparking.Web.NinoServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
