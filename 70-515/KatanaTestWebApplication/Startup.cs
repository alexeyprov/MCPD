using Owin;

namespace KatanaTestWebApplication
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseTestPage();
		}
	}
}
