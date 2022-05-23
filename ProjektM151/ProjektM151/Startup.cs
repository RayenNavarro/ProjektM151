using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ProjektM151.StartupOwin))]

namespace ProjektM151
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);
        }
    }
}
