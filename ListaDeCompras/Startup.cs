using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ListaDeCompras.Startup))]
namespace ListaDeCompras
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
