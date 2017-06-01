// ----------------------------------------------------------------------------------------------
// <copyright file="RegistrarTiempos.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Amezquita.ControlTiempos.Dominio;
using Amezquita.ControlTiempos.Infraestructura;
using Amezquita.ControlTiempos.Infraestructura.Cache;
using Amezquita.ControlTiempos.Infraestructura.Datos;
using MediatR;

namespace Amezquita.ControlTiempos.Features.Cargues
{
    public class RegistrarTiempos
    {
        #region Nested type: Command

        public class Command : IAsyncRequest
        {
            #region Instance Properties

            [Display(Name = "Actividad")]
            [Required(ErrorMessage = "La actividad es obligatoria")]
            public Guid ActividadId { get; set; }

            [Display(Name = "Fecha")]
            [Required(ErrorMessage = "La fecha es obligatoria")]
            public DateTime Fecha { get; set; }

            [Display(Name = "Hora Fin")]
            [Required(ErrorMessage = "La hora de fin es obligatoria")]
            public TimeSpan HoraFin { get; set; }

            [Display(Name = "Hora Inicio")]
            [Required(ErrorMessage = "La hora de inicio es obligatoria")]
            public TimeSpan HoraInicio { get; set; }

            [PropertyModelBinder(typeof (NewGuidModelBinder))]
            public Guid Id { get; set; }

            [Display(Name = "Observación")]
            [MaxLength(512, ErrorMessage = "La observación debe ser máximo de 512 caracteres.")]
            public string Observaciones { get; set; }

            [Display(Name = "Proyecto")]
            [Required(ErrorMessage = "El proyecto es obligatorio")]
            public Guid ProyectoId { get; set; }

            [Display(Name = "Servicio")]
            [Required(ErrorMessage = "El servicio es obligatorio")]
            public Guid ServicioId { get; set; }

            [Display(Name = "Usuario")]
            [Required(ErrorMessage = "El usuario es obligatorio")]
            [PropertyModelBinder(typeof (UserIdModelBinder))]
            public Guid UsuarioId { get; set; }

            #endregion
        }

        #endregion

        #region Nested type: Handler

        public class Handler : AsyncRequestHandler<Command>
        {
            #region Readonly & Static Fields

            private readonly IManejadorDeCache _cache;
            private readonly ControlTiemposDbContext _unidadDeTrabajo;

            #endregion

            #region C'tors

            public Handler(ControlTiemposDbContext unidadDeTrabajo, IManejadorDeCache cache)
            {
                _unidadDeTrabajo = unidadDeTrabajo;
                _cache = cache;
            }

            #endregion

            #region Instance Methods

            protected override async Task HandleCore(Command message)
            {
                var proyecto = await _unidadDeTrabajo.Proyectos
                                                     .Include(p => p.Servicios)
                                                     .Include(p => p.Actividades)
                                                     .Include(p => p.GrupoTrabajo)
                                                     .Include(p => p.GrupoTrabajo.Select(gt => gt.ReglaCargue))
                                                     .Where(p => p.Id == message.ProyectoId)
                                                     .FirstOrDefaultAsync();

                if (proyecto == null)
                {
                    throw new InvalidOperationException("El proyecto no existe.");
                }

                if (!proyecto.ServicioAsociado(message.ServicioId))
                {
                    throw new InvalidOperationException("El servicio no está asociado al proyecto.");
                }

                if (!proyecto.ActividadAsociada(message.ActividadId))
                {
                    throw new InvalidOperationException("La actividad no está asociada al proyecto.");
                }

                if (!proyecto.UsuarioPerteneceAlGrupo(message.UsuarioId))
                {
                    throw new InvalidOperationException("El usuario no pertecene al grupo de trabajo.");
                }

                var servicio = proyecto.Servicios.Single(s => s.Id == message.ServicioId);
                var actividad = proyecto.Actividades.Single(a => a.Id == message.ActividadId);
                var usuario = proyecto.GrupoTrabajo.Single(u => u.Id == message.UsuarioId);

                var cargueHoras = new CargueHoras(message.Id,
                    proyecto,
                    usuario,
                    servicio,
                    actividad,
                    message.Fecha,
                    message.HoraInicio,
                    message.HoraFin,
                    message.Observaciones);

                var existe = await _unidadDeTrabajo.CarguesHoras
                                                   .Where(ch => ch.RegistradaPor.Id == message.UsuarioId)
                                                   .Where(ch => ch.FechaInicio <= cargueHoras.FechaFin && cargueHoras.FechaInicio <= ch.FechaFin)
                                                   .FirstOrDefaultAsync();

                if (existe != null)
                {
                    if (!existe.EsNovedad)
                    {
                        throw new InvalidOperationException("El usuario ya tiene un registro en el rango de fechas.");
                    }
                    else
                    {
                        throw new InvalidOperationException("El usuario ya tiene una novedad en el rango de fechas.");
                    }
                }

                var fechaInicio = new DateTime(message.Fecha.Year, message.Fecha.Month, message.Fecha.Day, 0, 0, 0);
                var fechaFin = new DateTime(message.Fecha.Year, message.Fecha.Month, message.Fecha.Day, 23, 59, 59);

                var horasCargadas = await _unidadDeTrabajo.CarguesHoras
                                                          .Where(ch => !ch.EsNovedad)
                                                          .Where(ch => ch.RegistradaPor.Id == message.UsuarioId)
                                                          .Where(ch => ch.FechaInicio >= fechaInicio && ch.FechaInicio <= fechaFin)
                                                          .Where(ch => ch.FechaFin >= fechaInicio && ch.FechaFin <= fechaFin)
                                                          .SumAsync(ch => DbFunctions.DiffHours(ch.FechaInicio, ch.FechaFin));

                if ((horasCargadas == usuario.ReglaCargue.LimiteHoraFraccion) || (horasCargadas + cargueHoras.HoraFraccion > usuario.ReglaCargue.LimiteHoraFraccion))
                {
                    throw new InvalidOperationException($"No es posible cargar más de {usuario.ReglaCargue.LimiteHoraFraccion} horas por día.");
                }

                _unidadDeTrabajo.CarguesHoras.Add(cargueHoras);

                await _unidadDeTrabajo.SaveChangesAsync();

                var llave = $"U({message.UsuarioId})P({message.ProyectoId})S({message.ServicioId})A({message.ActividadId})";
                _cache.Eliminar(llave);

                if (actividad.NecesitaAprobacion)
                {
                    DomainEvents.Raise(new ActividadRegistrada(proyecto, actividad));
                }
            }

            #endregion
        }

        #endregion
    }
}
