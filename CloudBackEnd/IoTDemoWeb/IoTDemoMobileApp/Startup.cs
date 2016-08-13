using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(IoTDemoMobileApp.Startup))]

namespace IoTDemoMobileApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}