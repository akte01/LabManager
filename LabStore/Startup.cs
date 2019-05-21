using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LabStore.Startup))]
namespace LabStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
