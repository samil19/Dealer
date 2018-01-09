using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dealer.Startup))]
namespace Dealer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
