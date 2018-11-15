using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pickup.Startup))]
namespace Pickup
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
