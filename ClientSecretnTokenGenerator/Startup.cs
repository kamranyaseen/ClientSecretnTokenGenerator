using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ClientSecretnTokenGenerator.Startup))]

namespace ClientSecretnTokenGenerator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
