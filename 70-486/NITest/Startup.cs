using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NITest.Startup))]
namespace NITest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
