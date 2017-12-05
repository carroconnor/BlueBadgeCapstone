using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Messenger.Web.Startup))]
namespace Messenger.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
