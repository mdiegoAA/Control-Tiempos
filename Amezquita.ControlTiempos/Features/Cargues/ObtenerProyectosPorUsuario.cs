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
    public class ObtenerProyectosPorUsuario
    {
        public class ProyectoDTO
        {
            public Guid Id { get; set; }
            public string Nombre { get; set; }
        }

        public class Query : IAsyncRequest<IEnumerable<ProyectoDTO>>
        {
            [PropertyModelBinder(typeof(UserIdModelBinder))]
            public Guid Id { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Query, IEnumerable<ProyectoDTO>>
        {
            private readonly ControlTiemposDbContext _unidadDeTrabajo;

            public Handler(ControlTiemposDbContext unidadDeTrabajo)
            {
                _unidadDeTrabajo = unidadDeTrabajo;
            }

            public async Task<IEnumerable<ProyectoDTO>> Handle(Query message)
            {
                var proyectos = await _unidadDeTrabajo.Proyectos
                                                      .Where(p => p.GrupoTrabajo.Any(u => u.Id == message.Id))
                                                      .OrderBy(p => p.Nombre)
                                                      .AsNoTracking()
                                                      .ToListAsync();

                return new List<ProyectoDTO>().InjectFrom(proyectos); 
            }
        }
    }
}