using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OOADProject.Startup))]
namespace OOADProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
