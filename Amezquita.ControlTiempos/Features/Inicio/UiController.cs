using System.Web.Mvc;

namespace Amezquita.ControlTiempos.Features.Inicio
{
    [Authorize]
    public class UiController : Controller
    {
        public ActionResult Index() => View();
    }
}