using Microsoft.Owin;
using Owin;

//[assembly: OwinStartupAttribute(typeof(NIBM.Procurement.Startup))]
namespace NIBM.Procurement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
