using Amezquita.ControlTiempos.Dominio;
using Amezquita.ControlTiempos.Infraestructura;
using Amezquita.ControlTiempos.Infraestructura.Datos;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Amezquita.ControlTiempos.Features.Cargues
{
    public class RegistrarNovedad
    {
        public class Command : IAsyncRequest
        {
            [Display(Name = "Actividad")]
            [Required(ErrorMessage = "La actividad es obligatoria")]
            public Guid ActividadId { get; set; }

            [Display(Name = "Fecha Inicio")]
            [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
            public DateTime FechaInicio { get; set; }

            [Display(Name = "Fecha Fin")]
            [Required(ErrorMessage = "La fecha de fin es obligatoria")]
            public DateTime FechaFin { get; set; }

            [PropertyModelBinder(typeof(NewGuidModelBinder))]
            public Guid Id { get; set; }

            [Display(Name = "Observación")]
            [MaxLength(512, ErrorMessage = "La observación debe ser máximo de 512 caracteres.")]
            public string Observacion { get; set; }

            [Display(Name = "Proyecto")]
            [Required(ErrorMessage = "El proyecto es obligatorio")]
            public Guid ProyectoId { get; set; }

            [Display(Name = "Servicio")]
            [Required(ErrorMessage = "El servicio es obligatorio")]
            public Guid ServicioId { get; set; }

            [Display(Name = "Usuario")]
            [Required(ErrorMessage = "El usuario es obligatorio")]
            public Guid UsuarioId { get; set; }

            [PropertyModelBinder(typeof(UserIdModelBinder))]
            public Guid RegistradaPorId { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly ControlTiemposDbContext _unidadDeTrabajo;

            public Handler(ControlTiemposDbContext unidadDeTrabajo)
            {
                _unidadDeTrabajo = unidadDeTrabajo;
            }

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
                    throw new InvalidOperationException("El proyecto no existe.");

                if (!proyecto.ServicioAsociado(message.ServicioId))
                    throw new InvalidOperationException("El servicio no está asociado al proyecto.");

                if (!proyecto.ActividadAsociada(message.ActividadId))
                    throw new InvalidOperationException("La actividad no está asociada al proyecto.");

                if (!proyecto.UsuarioPerteneceAlGrupo(message.UsuarioId))
                    throw new InvalidOperationException("El usuario no pertecene al grupo de trabajo.");

                var registradaPor = await _unidadDeTrabajo.Usuarios.FirstOrDefaultAsync(u => u.Id == message.RegistradaPorId);

                if (registradaPor == null)
                    throw new InvalidOperationException("El usuario que registra el cargue de horas no existe.");

                var servicio = proyecto.Servicios.Single(s => s.Id == message.ServicioId);
                var actividad = proyecto.Actividades.Single(a => a.Id == message.ActividadId);
                var usuario = proyecto.GrupoTrabajo.Single(u => u.Id == message.UsuarioId);

                var cargueHoras = new CargueHoras(message.Id,
                                                  proyecto,
                                                  usuario,
                                                  servicio,
                                                  actividad,
                                                  message.FechaInicio,
                                                  message.FechaFin,
                                                  message.Observacion,
                                                  registradaPor);

                var existe = await _unidadDeTrabajo.CarguesHoras
                                                   .Where(ch => ch.EsNovedad)
                                                   .Where(ch => ch.Usuario.Id == message.UsuarioId)
                                                   .Where(ch => ch.FechaInicio <= cargueHoras.FechaInicio && cargueHoras.FechaFin <= ch.FechaFin)
                                                   .AnyAsync();

                if (existe)
                    throw new InvalidOperationException("El usuario ya tiene una novedad registrada en el rango de fechas.");

                _unidadDeTrabajo.CarguesHoras.Add(cargueHoras);

                await _unidadDeTrabajo.SaveChangesAsync();
            }
        }
    }
}