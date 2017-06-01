using MediatR;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Amezquita.ControlTiempos.Areas.Administracion.Features.Actividades
{
    [Authorize(Roles = "Administrador")]
    public class UiController : Controller
    {
        private readonly IMediator _mediator;

        public UiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public ActionResult Index() => View();

        [HttpPost]
        public async Task<JsonCamelCaseResult> Eliminar(Eliminar.Command command)
        {
            await _mediator.SendAsync(command);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerPorId(ObtenerActividadPorId.Query query)
        {
            var actividadDTO = await _mediator.SendAsync(query);
            return new JsonCamelCaseResult(actividadDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerPorPagina(ObtenerActividadesPorPagina.Query query)
        {
            var paginaDTO = await _mediator.SendAsync(query);
            return new JsonCamelCaseResult(paginaDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonCamelCaseResult> Crear(Crear.Command command)
        {
            await _mediator.SendAsync(command);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public async Task<JsonCamelCaseResult> Editar(Editar.Command command)
        {
            await _mediator.SendAsync(command);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }
    }
}