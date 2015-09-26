using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookLocations.Startup))]
namespace BookLocations
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
