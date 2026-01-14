using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TourismManagementSystem.Startup))]
namespace TourismManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
