using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Import_to_DB.StartupOwin))]

namespace Import_to_DB
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);
        }
    }
}
