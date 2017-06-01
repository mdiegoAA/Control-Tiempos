using Amezquita.ControlTiempos.Aplicacion.Administracion.Calendarios;
using Amezquita.ControlTiempos.Infraestructura;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Amezquita.ControlTiempos.Areas.Administracion.Features.Calendarios
{
    [Authorize(Roles = "Administrador")]
    public class UiController : Controller
    {
        private readonly IGeneradorIdentidad<Guid> _identidad;
        private readonly IServicioCalendarios _servicio;

        public UiController(IServicioCalendarios servicio, IGeneradorIdentidad<Guid> identidad)
        {
            _servicio = servicio;
            _identidad = identidad;
        }

        public ActionResult Index() => View();

        [HttpPost]
        public async Task<JsonCamelCaseResult> Eliminar(Guid id)
        {
            var dto = new EliminarDiaCalendarioDTO
            {
                Id = id
            };

            await _servicio.EliminarDiaCalendarioAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerPorId(Guid id)
        {
            var dto = new ObtenerDiaCalendarioDTO
            {
                Id = id
            };

            var diaDTO = await _servicio.ObtenerDiaCalendarioAsync(dto);
            return new JsonCamelCaseResult(diaDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerPorPagina(int numeroPagina, int registrosPagina, string buscar)
        {
            var dto = new ObtenerDiasCalendarioPorPaginaDTO
            {
                Buscar = buscar,
                NumeroPagina = numeroPagina,
                RegistrosPagina = registrosPagina
            };

            var paginaDTO = await _servicio.ObtenerDiasCalendarioPorPaginasAsync(dto);
            return new JsonCamelCaseResult(paginaDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonCamelCaseResult> Crear(GuardarDiaCalendarioDTO dto)
        {
            dto.Id = _identidad.GenerarId();

            await _servicio.CrearDiaCalendarioAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public async Task<JsonCamelCaseResult> Editar(GuardarDiaCalendarioDTO dto)
        {
            await _servicio.EditarDiaCalendarioAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }
    }
}