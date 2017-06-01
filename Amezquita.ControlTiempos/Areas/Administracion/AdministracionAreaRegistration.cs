using System.Web.Mvc;

namespace Amezquita.ControlTiempos.Areas.Administracion
{
    public class AdministracionAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Administracion";

        public override void RegisterArea(AreaRegistrationContext context) => context.MapRoute("Administracion_default", "Administracion/{controller}/{action}/{id}", new { action = "Index", id = UrlParameter.Optional });
    }
}