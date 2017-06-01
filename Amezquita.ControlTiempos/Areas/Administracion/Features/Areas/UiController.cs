using Amezquita.ControlTiempos.Aplicacion.Administracion.Areas;
using Amezquita.ControlTiempos.Infraestructura;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Amezquita.ControlTiempos.Areas.Administracion.Features.Areas
{
    [Authorize(Roles = "Administrador")]
    public class UiController : Controller
    {
        private readonly IGeneradorIdentidad<Guid> _identidad;
        private readonly IServicioAreas _servicio;

        public UiController(IServicioAreas servicio, IGeneradorIdentidad<Guid> identidad)
        {
            _servicio = servicio;
            _identidad = identidad;
        }

        public ActionResult Index() => View();

        [HttpPost]
        public async Task<JsonCamelCaseResult> Eliminar(Guid id)
        {
            var dto = new EliminarAreaDTO
            {
                Id = id
            };

            await _servicio.EliminarAreaAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerPorId(Guid id)
        {
            var dto = new ObtenerAreaPorIdDTO
            {
                Id = id
            };

            var areaDTO = await _servicio.ObtenerAreaPorIdAsync(dto);
            return new JsonCamelCaseResult(areaDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerPorPagina(int numeroPagina, int registrosPagina, string buscar)
        {
            var dto = new ObtenerAreasPorPaginaDTO
            {
                Buscar = buscar,
                NumeroPagina = numeroPagina,
                RegistrosPagina = registrosPagina
            };

            var paginaDTO = await _servicio.ObtenerAreasPorPaginaAsync(dto);
            return new JsonCamelCaseResult(paginaDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonCamelCaseResult> Crear(GuardarAreaDTO dto)
        {
            dto.Id = _identidad.GenerarId();

            await _servicio.CrearAreaAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public async Task<JsonCamelCaseResult> Editar(GuardarAreaDTO dto)
        {
            await _servicio.EditarAreaAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }
    }
}