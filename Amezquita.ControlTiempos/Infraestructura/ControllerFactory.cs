using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Amezquita.ControlTiempos.Infraestructura
{
    public class ControllerFactory : DefaultControllerFactory
    {
        protected override Type GetControllerType(RequestContext requestContext, string controllerName)
        {
            var areaName = requestContext.RouteData.DataTokens["area"] as string;
            var areaNamespace = !string.IsNullOrEmpty(areaName) ? "Areas." + areaName + "." : string.Empty;
            var controllerType = string.Format("Amezquita.ControlTiempos.{1}Features.{0}.UiController", controllerName, areaNamespace);

            return typeof(ControllerFactory).Assembly.GetType(controllerType);
        }
    }
}