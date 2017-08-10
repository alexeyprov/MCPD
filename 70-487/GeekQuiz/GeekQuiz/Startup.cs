using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(GeekQuiz.Startup))]
namespace GeekQuiz
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
