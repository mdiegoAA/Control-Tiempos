using Amezquita.ControlTiempos.Aplicacion.Administracion.Usuarios;
using Amezquita.ControlTiempos.Infraestructura;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Amezquita.ControlTiempos.Areas.Administracion.Features.Usuarios
{
    [Authorize(Roles = "Administrador")]
    public class UiController : Controller
    {
        private readonly IGeneradorIdentidad<Guid> _identidad;
        private readonly IServicioUsuarios _servicio;

        public UiController(IServicioUsuarios servicio, IGeneradorIdentidad<Guid> identidad)
        {
            _servicio = servicio;
            _identidad = identidad;
        }

        public ActionResult Index() => View();

        [HttpPost]
        public async Task<JsonCamelCaseResult> Eliminar(Guid id)
        {
            var dto = new EliminarUsuarioDTO
            {
                Id = id
            };

            await _servicio.EliminarUsuarioAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerPorId(Guid id)
        {
            var dto = new ObtenerUsuarioPorIdDTO
            {
                Id = id
            };

            var usuarioDTO = await _servicio.ObtenerUsuarioPorIdAsync(dto);
            return new JsonCamelCaseResult(usuarioDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerPorPagina(int numeroPagina, int registrosPagina, string buscar)
        {
            var dto = new ObtenerUsuariosPorPaginaDTO
            {
                Buscar = buscar,
                NumeroPagina = numeroPagina,
                RegistrosPagina = registrosPagina
            };

            var paginaDTO = await _servicio.ObtenerUsuariosPorPaginaAsync(dto);
            return new JsonCamelCaseResult(paginaDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonCamelCaseResult> Crear(GuardarUsuarioDTO dto)
        {
            dto.Id = _identidad.GenerarId();

            await _servicio.CrearUsuarioAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public async Task<JsonCamelCaseResult> Editar(GuardarUsuarioDTO dto)
        {
            await _servicio.EditarUsuarioAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public async Task<JsonCamelCaseResult> GuardarRolesUsuario(GuardarRolesUsuarioDTO dto)
        {
            await _servicio.GuardarRolesUsuarioAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerCargos()
        {
            var cargosDTO = await _servicio.ObtenerCargosAsync();
            return new JsonCamelCaseResult(cargosDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerRoles()
        {
            var rolesDTO = await _servicio.ObtenerRolesAsync();
            return new JsonCamelCaseResult(rolesDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerRolesUsuario(Guid id)
        {
            var rolesDTO = await _servicio.ObtenerRolesPorUsuarioAsync(new ObteneRolesPorUsuarioDTO
            {
                Id = id
            });

            return new JsonCamelCaseResult(rolesDTO, JsonRequestBehavior.AllowGet);
        }
    }
}