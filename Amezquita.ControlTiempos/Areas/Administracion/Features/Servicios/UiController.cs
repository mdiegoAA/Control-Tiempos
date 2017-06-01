using Amezquita.ControlTiempos.Aplicacion.Administracion.Servicios;
using Amezquita.ControlTiempos.Infraestructura;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Amezquita.ControlTiempos.Areas.Administracion.Features.Servicios
{
    [Authorize(Roles = "Administrador")]
    public class UiController : Controller
    {
        private readonly IGeneradorIdentidad<Guid> _identidad;
        private readonly IServicioServicios _servicio;

        public UiController(IServicioServicios servicio, IGeneradorIdentidad<Guid> identidad)
        {
            _servicio = servicio;
            _identidad = identidad;
        }

        public ActionResult Index() => View();

        [HttpPost]
        public async Task<JsonCamelCaseResult> Eliminar(Guid id)
        {
            var dto = new EliminarServicioDTO
            {
                Id = id
            };

            await _servicio.EliminarServicioAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerPorId(Guid id)
        {
            var dto = new ObtenerServicioPorIdDTO
            {
                Id = id
            };

            var servicioDTO = await _servicio.ObtenerServicioPorIdAsync(dto);
            return new JsonCamelCaseResult(servicioDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerPorPagina(int numeroPagina, int registrosPagina, string buscar)
        {
            var dto = new ObtenerServiciosPorPaginaDTO
            {
                Buscar = buscar,
                NumeroPagina = numeroPagina,
                RegistrosPagina = registrosPagina
            };

            var paginaDTO = await _servicio.ObtenerServiciosPorPaginaAsync(dto);
            return new JsonCamelCaseResult(paginaDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonCamelCaseResult> Crear(GuardarServicioDTO dto)
        {
            dto.Id = _identidad.GenerarId();

            await _servicio.CrearServicioAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public async Task<JsonCamelCaseResult> Editar(GuardarServicioDTO dto)
        {
            await _servicio.EditarServicioAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }
    }
}