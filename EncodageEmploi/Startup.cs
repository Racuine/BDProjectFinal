using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EncodageEmploi.Startup))]
namespace EncodageEmploi
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
