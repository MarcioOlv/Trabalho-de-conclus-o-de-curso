using System.Web;
using System.Web.Mvc;

namespace Gerenciador_de_Consultas_Médicas
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorHandler.AiHandleErrorAttribute());
        }
    }
}
