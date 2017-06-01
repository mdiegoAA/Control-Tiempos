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
    public class ObtenerServiciosPorProyecto
    {
        public class ServicioDTO
        {
            public Guid Id { get; set; }
            public string Nombre { get; set; }
        }

        public class Query : IAsyncRequest<IEnumerable<ServicioDTO>>
        {
            public Guid Id { get; set; }

            [PropertyModelBinder(typeof(UserIdModelBinder))]
            public Guid UsuarioId { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Query, IEnumerable<ServicioDTO>>
        {
            private readonly ControlTiemposDbContext _unidadDeTrabajo;

            public Handler(ControlTiemposDbContext unidadDeTrabajo)
            {
                _unidadDeTrabajo = unidadDeTrabajo;
            }

            public async Task<IEnumerable<ServicioDTO>> Handle(Query message)
            {
                var proyecto = await _unidadDeTrabajo.Proyectos
                                                     .Include(p => p.Auditor)
                                                     .Include(p => p.Director)
                                                     .Include(p => p.Gerente)
                                                     .Include(p => p.Supervisor)
                                                     .Include(p => p.Servicios)
                                                     .Include(p => p.Actividades)
                                                     .FirstOrDefaultAsync(p => p.Id == message.Id);

                List<Servicio> servicios;

                if (proyecto.UsuarioEsAdministrador(message.UsuarioId))
                    servicios = proyecto.Servicios
                                        .OrderBy(s => s.Nombre)
                                        .ToList();
                else
                    servicios = proyecto.Servicios
                                        .Where(s => !s.EsGenerico)
                                        .OrderBy(s => s.Nombre)
                                        .ToList();

                return new List<ServicioDTO>().InjectFrom(servicios);
            }
        }
    }
}