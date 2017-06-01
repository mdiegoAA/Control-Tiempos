using Amezquita.ControlTiempos.Aplicacion.Administracion.Clientes;
using Amezquita.ControlTiempos.Infraestructura;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Amezquita.ControlTiempos.Areas.Administracion.Features.Clientes
{
    [Authorize(Roles = "Administrador")]
    public class UiController : Controller
    {
        private readonly IGeneradorIdentidad<Guid> _identidad;
        private readonly IServicioClientes _servicio;

        public UiController(IServicioClientes servicio, IGeneradorIdentidad<Guid> identidad)
        {
            _servicio = servicio;
            _identidad = identidad;
        }

        public ActionResult Index() => View();

        [HttpPost]
        public async Task<JsonCamelCaseResult> Eliminar(Guid id)
        {
            var dto = new EliminarClienteDTO
            {
                Id = id
            };

            await _servicio.EliminarClienteAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerPorId(Guid id)
        {
            var dto = new ObtenerClientePorIdDTO
            {
                Id = id
            };

            var clienteDTO = await _servicio.ObtenerClientesPorIdAsync(dto);
            return new JsonCamelCaseResult(clienteDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerPorPagina(int numeroPagina, int registrosPagina, string buscar)
        {
            var dto = new ObtenerClientesPorPaginaDTO
            {
                Buscar = buscar,
                NumeroPagina = numeroPagina,
                RegistrosPagina = registrosPagina
            };

            var paginaDTO = await _servicio.ObtenerClientesPorPaginaAsync(dto);
            return new JsonCamelCaseResult(paginaDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonCamelCaseResult> Crear(GuardarClienteDTO dto)
        {
            dto.Id = _identidad.GenerarId();

            await _servicio.CrearClienteAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public async Task<JsonCamelCaseResult> Editar(GuardarClienteDTO dto)
        {
            await _servicio.EditarClienteAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }
    }
}