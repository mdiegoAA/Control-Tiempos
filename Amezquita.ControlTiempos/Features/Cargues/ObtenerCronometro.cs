using Amezquita.ControlTiempos.Infraestructura;
using Amezquita.ControlTiempos.Infraestructura.Datos;
using Amezquita.ControlTiempos.Infraestructura.Cache;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Amezquita.ControlTiempos.Features.Cargues
{
    public class ObtenerCronometro
    {
        public class CronometroDTO
        {
            public Guid ActividadId { get; set; }
            public DateTime Inicio { get; set; }
            public Guid ProyectoId { get; set; }
            public Guid ServicioId { get; set; }
            public Guid UsuarioId { get; set; }
        }

        public class Query : IAsyncRequest<CronometroDTO>
        {
            public Guid ActividadId { get; set; }
            public Guid ProyectoId { get; set; }
            public Guid ServicioId { get; set; }

            [PropertyModelBinder(typeof(UserIdModelBinder))]
            public Guid UsuarioId { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Query, CronometroDTO>
        {
            private readonly ControlTiemposDbContext _unidadDeTrabajo;
            private readonly IManejadorDeCache _cache;

            public Handler(ControlTiemposDbContext unidadDeTrabajo, IManejadorDeCache cache)
            {
                _unidadDeTrabajo = unidadDeTrabajo;
                _cache = cache;
            }

            public Task<CronometroDTO> Handle(Query message)
            {
                var llave = $"U({message.UsuarioId})P({message.ProyectoId})S({message.ServicioId})A({message.ActividadId})";
                return Task.FromResult(_cache.Obtener<CronometroDTO>(llave));
            }
        }
    }
}