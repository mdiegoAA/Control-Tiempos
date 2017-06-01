using System.Web.Mvc;

namespace Amezquita.ControlTiempos.Areas.Administracion.Features.Inicio
{
    [Authorize(Roles = "Administrador")]
    public class UiController : Controller
    {
        public ActionResult Index() => View();
    }
}