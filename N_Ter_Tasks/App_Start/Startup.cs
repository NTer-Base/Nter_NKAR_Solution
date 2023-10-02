using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(N_Ter.Security.Azure_AD.Startup))]

namespace N_Ter.Security.Azure_AD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

