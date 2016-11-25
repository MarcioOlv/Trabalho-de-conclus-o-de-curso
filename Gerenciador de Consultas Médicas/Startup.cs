using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gerenciador_de_Consultas_Médicas.Startup))]
namespace Gerenciador_de_Consultas_Médicas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
