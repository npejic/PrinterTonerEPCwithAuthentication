using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PrinterTonerEPCwithAuthentication.Startup))]
namespace PrinterTonerEPCwithAuthentication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
