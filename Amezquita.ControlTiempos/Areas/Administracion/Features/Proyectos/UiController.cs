using Amezquita.ControlTiempos.Aplicacion.Administracion.Proyectos;
using Amezquita.ControlTiempos.Infraestructura;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Amezquita.ControlTiempos.Areas.Administracion.Features.Proyectos
{
    [Authorize(Roles = "Administrador")]
    public class UiController : Controller
    {
        private readonly IGeneradorIdentidad<Guid> _identidad;
        private readonly IServicioProyectos _servicio;

        public UiController(IServicioProyectos servicio, IGeneradorIdentidad<Guid> identidad)
        {
            _servicio = servicio;
            _identidad = identidad;
        }

        public ActionResult Index() => View();

        [HttpPost]
        public async Task<JsonCamelCaseResult> Eliminar(Guid id)
        {
            var dto = new EliminarProyectoDTO
            {
                Id = id
            };

            await _servicio.EliminarProyectoAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerPorId(Guid id)
        {
            var dto = new ObtenerProyectoPorIdDTO
            {
                Id = id
            };

            var actividadDTO = await _servicio.ObtenerProyectoPorIdAsync(dto);
            return new JsonCamelCaseResult(actividadDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerPorPagina(int numeroPagina, int registrosPagina, string buscar)
        {
            var dto = new ObtenerProyectosPorPaginaDTO
            {
                Buscar = buscar,
                NumeroPagina = numeroPagina,
                RegistrosPagina = registrosPagina
            };

            var paginaDTO = await _servicio.ObtenerProyectosPorPaginaAsync(dto);
            return new JsonCamelCaseResult(paginaDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonCamelCaseResult> GuardarActividadesProyecto(GuardarActividadesProyectoDTO dto)
        {
            await _servicio.GuardarActividadesProyectoAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public async Task<JsonCamelCaseResult> GuardarAreasProyecto(GuardarAreasProyectoDTO dto)
        {
            await _servicio.GuardarAreasProyectoAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public async Task<JsonCamelCaseResult> GuardarGrupoDeTrabajoProyecto(GuardarGrupoDeTrabajoProyectoDTO dto)
        {
            await _servicio.GuardarGrupoDeTrabajoProyectoAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public async Task<JsonCamelCaseResult> GuardarServiciosProyecto(GuardarServiciosProyectoDTO dto)
        {
            await _servicio.GuardarServiciosProyectoAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerActividades()
        {
            var actividadesDTO = await _servicio.ObtenerActvidadesAsync();
            return new JsonCamelCaseResult(actividadesDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerActividadesPorProyecto(Guid id)
        {
            var actividadesDTO = await _servicio.ObtenerActvidadesPorProyectoAsync(new ObtenerActividadesPorProyectoDTO
            {
                Id = id
            });

            return new JsonCamelCaseResult(actividadesDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerAreas()
        {
            var areasDTO = await _servicio.ObtenerAreasAsync();
            return new JsonCamelCaseResult(areasDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerAreasPorProyecto(Guid id)
        {
            var areasProyectoDTO = await _servicio.ObtenerAreasProyectoAsync(new ObtenerAreasProyectoDTO
            {
                Id = id
            });

            return new JsonCamelCaseResult(areasProyectoDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerClientes()
        {
            var clientesDTO = await _servicio.ObtenerClientesAsync();
            return new JsonCamelCaseResult(clientesDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerGrupoDeTrabajoPorProyecto(Guid id)
        {
            var usuariosDTO = await _servicio.ObtenerGrupoDeTrabajoPorProyectoAsync(new ObtenerGrupoDeTrabajoPorProyectoDTO
            {
                Id = id
            });

            return new JsonCamelCaseResult(usuariosDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerServicios()
        {
            var serviciosDTO = await _servicio.ObtenerServiciosAsync();
            return new JsonCamelCaseResult(serviciosDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerServiciosPorProyecto(Guid id)
        {
            var serviciosDTO = await _servicio.ObtenerServiciosPorProyectoAsync(new ObtenerServiciosPorProyectoDTO
            {
                Id = id
            });

            return new JsonCamelCaseResult(serviciosDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerUsuarios()
        {
            var usuariosDTO = await _servicio.ObtenerUsuariosAsync();
            return new JsonCamelCaseResult(usuariosDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonCamelCaseResult> Crear(GuardarProyectoDTO dto)
        {
            dto.Id = _identidad.GenerarId();

            await _servicio.CrearProyectoAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public async Task<JsonCamelCaseResult> Editar(GuardarProyectoDTO dto)
        {
            await _servicio.EditarProyectoAsync(dto);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }
    }
}