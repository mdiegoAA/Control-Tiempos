// ----------------------------------------------------------------------------------------------
// <copyright file="UiController.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using MediatR;

namespace Amezquita.ControlTiempos.Features.Cargues
{
    public class UiController : Controller
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region C'tors

        public UiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Instance Methods

        [HttpGet]
        public async Task<JsonCamelCaseResult> ObtenerPorId(ObtenerCarguesPorId.Query query)
        {
            var registrarDTO = await _mediator.SendAsync(query);
            return new JsonCamelCaseResult(registrarDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Director")]
        public ActionResult Aprobar() => View();

        [HttpGet]
        [Authorize(Roles = "Administrador,Director")]
        public ActionResult Prueba() => View();

        [HttpPost]
        [Authorize(Roles = "Administrador,Director")]
        public async Task<JsonCamelCaseResult> AprobarHoras(AprobarCargue.Command command)
        {
            await _mediator.SendAsync(command);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Director,Operativo,Recursos Humanos")]
        public async Task<JsonCamelCaseResult> CargarHoras(RegistrarTiempos.Command command)
        {
            await _mediator.SendAsync(command);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Director,Operativo,Recursos Humanos")]
        public async Task<JsonCamelCaseResult> ModificarControlTiempos(ModificarRegistrarTiempos.Command command)
        {
            await _mediator.SendAsync(command);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Director,Operativo,Recursos Humanos")]
        public ActionResult Historial() => View();

        [HttpGet]
        [Authorize(Roles = "Administrador,Director,Operativo,Recursos Humanos")]
        public ActionResult Index() => View();

        [HttpPost]
        [Authorize(Roles = "Administrador,Director,Operativo,Recursos Humanos")]
        public async Task<JsonCamelCaseResult> IniciarCronometro(IniciarCronometro.Command command)
        {
            await _mediator.SendAsync(command);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Director,Operativo,Recursos Humanos")]
        public ActionResult ModificarRegistrarTiempos() => View();

        [HttpGet]
        [Authorize(Roles = "Administrador,Director,Operativo,Recursos Humanos")]
        public async Task<JsonCamelCaseResult> ObtenerActividades(ObtenerActividadesPorProyecto.Query query)
        {
            var dtos = await _mediator.SendAsync(query);
            return new JsonCamelCaseResult(dtos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Director,Operativo,Recursos Humanos")]
        public async Task<JsonCamelCaseResult> ObtenerActividadesRegistradas(ObtenerCarguesPorFechas.Query query)
        {
            var paginaDTO = await _mediator.SendAsync(query);
            return new JsonCamelCaseResult(paginaDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Director,Operativo,Recursos Humanos")]
        public async Task<JsonCamelCaseResult> ObtenerCronometro(ObtenerCronometro.Query query)
        {
            var cronometroDTO = await _mediator.SendAsync(query);
            return new JsonCamelCaseResult(cronometroDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Director,Operativo,Recursos Humanos")]
        public async Task<JsonCamelCaseResult> ObtenerHistorial(ObtenerCarguesPorUsuario.Query query)
        {
            var paginaDTO = await _mediator.SendAsync(query);
            return new JsonCamelCaseResult(paginaDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Operativo,Director")]
        public async Task<JsonCamelCaseResult> ObtenerHorasPorAprobar(ObtenerCarguesPorAprobar.Query query)
        {
            var paginaDTO = await _mediator.SendAsync(query);
            return new JsonCamelCaseResult(paginaDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Director,Operativo,Recursos Humanos")]
        public async Task<JsonCamelCaseResult> ObtenerProyectos(ObtenerProyectosPorUsuario.Query query)
        {
            var dtos = await _mediator.SendAsync(query);
            return new JsonCamelCaseResult(dtos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Director,Operativo,Recursos Humanos")]
        public async Task<JsonCamelCaseResult> ObtenerProyectosPorUsuario(ObtenerProyectosPorUsuario.Query query)
        {
            var dtos = await _mediator.SendAsync(query);
            return new JsonCamelCaseResult(dtos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Director,Operativo,Recursos Humanos")]
        public async Task<JsonCamelCaseResult> ObtenerServicios(ObtenerServiciosPorProyecto.Query query)
        {
            var dtos = await _mediator.SendAsync(query);
            return new JsonCamelCaseResult(dtos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Director,Operativo,Recursos Humanos")]
        public async Task<JsonCamelCaseResult> ObtenerUsuarios()
        {
            var dtos = await _mediator.SendAsync(new ObtenerUsuarios.Query());
            return new JsonCamelCaseResult(dtos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Director,Operativo,Recursos Humanos")]
        public async Task<JsonCamelCaseResult> RegistrarNovedad(RegistrarNovedad.Command command)
        {
            await _mediator.SendAsync(command);
            return new JsonCamelCaseResult(null, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Recursos Humanos")]
        public ActionResult RegistrarNovedades() => View();

        [HttpGet]
        [Authorize(Roles = "Administrador,Director,Operativo,Recursos Humanos")]
        public ActionResult RegistrarTiempos() => View();

        #endregion
    }
}
