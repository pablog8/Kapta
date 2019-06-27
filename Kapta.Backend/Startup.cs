using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Kapta.Backend.Startup))]
namespace Kapta.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
