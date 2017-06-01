using Amezquita.ControlTiempos.Dominio;
using Amezquita.ControlTiempos.Infraestructura;
using Amezquita.ControlTiempos.Infraestructura.Datos;
using Amezquita.ControlTiempos.Infraestructura.Extensiones;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Amezquita.ControlTiempos.Features.Cargues
{
    public class ObtenerActividadesPorProyecto
    {
        public class ActividadDTO
        {
            public Guid Id { get; set; }
            public string Nombre { get; set; }
        }

        public class Query : IAsyncRequest<IEnumerable<ActividadDTO>>
        {
            public Guid Id { get; set; }

            [PropertyModelBinder(typeof(UserIdModelBinder))]
            public Guid UsuarioId { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Query, IEnumerable<ActividadDTO>>
        {
            private readonly ControlTiemposDbContext _unidadDeTrabajo;

            public Handler(ControlTiemposDbContext unidadDeTrabajo)
            {
                _unidadDeTrabajo = unidadDeTrabajo;
            }

            public async Task<IEnumerable<ActividadDTO>> Handle(Query message)
            {
                var proyecto = await _unidadDeTrabajo.Proyectos
                                                     .Include(p => p.Auditor)
                                                     .Include(p => p.Director)
                                                     .Include(p => p.Gerente)
                                                     .Include(p => p.Supervisor)
                                                     .Include(p => p.Servicios)
                                                     .Include(p => p.Actividades)
                                                     .FirstOrDefaultAsync(p => p.Id == message.Id);

                List<Actividad> actividades;

                if (proyecto.UsuarioEsAdministrador(message.UsuarioId))
                    actividades = proyecto.Actividades
                                          .OrderBy(a => a.Nombre)
                                          .ToList();
                else
                    actividades = proyecto.Actividades
                                          .Where(s => !s.EsGenerica)
                                          .OrderBy(a => a.Nombre)
                                          .ToList();

                return new List<ActividadDTO>().InjectFrom(actividades);
            }
        }
    }
}