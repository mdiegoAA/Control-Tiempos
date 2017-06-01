// ----------------------------------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos</project>
// ----------------------------------------------------------------------------------------------

using System.Web.Mvc;
using System.Web.Routing;

namespace Amezquita.ControlTiempos
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default",
                "{controller}/{action}/{id}",
                new
                {
                    controller = "Inicio",
                    action = "Index",
                    id = UrlParameter.Optional
                });
        }
    }
}