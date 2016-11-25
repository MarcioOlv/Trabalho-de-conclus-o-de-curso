using Gerenciador_de_Consultas_Médicas.Mappers;
using Gerenciador_de_Consultas_Médicas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Gerenciador_de_Consultas_Médicas
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //AutoMapperConfig.RegisterMappings();
        }

        //Criar DB
        //Database.SetInitializer<DominioContainer>(new InicializarBanco());
    }
}
