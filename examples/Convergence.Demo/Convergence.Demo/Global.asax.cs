using System.Web.Mvc;
using System.Web.Routing;

namespace Convergence.Demo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            UnityConfig.RegisterTypes(UnityConfig.Container);
        }
    }
}
