using Amezquita.ControlTiempos.Infraestructura;
using Amezquita.ControlTiempos.Infraestructura.Datos;
using Amezquita.ControlTiempos.Infraestructura.Cache;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Amezquita.ControlTiempos.Features.Cargues
{
    public class IniciarCronometro
    {
        public class Command : IAsyncRequest
        {
            public Guid ActividadId { get; set; }
            public Guid ProyectoId { get; set; }
            public Guid ServicioId { get; set; }

            [PropertyModelBinder(typeof(UserIdModelBinder))]
            public Guid UsuarioId { get; set; }
        }

        public class CronometroDTO
        {
            public Guid ActividadId { get; set; }
            public DateTime Inicio { get; set; }
            public Guid ProyectoId { get; set; }
            public Guid ServicioId { get; set; }
            public Guid UsuarioId { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly ControlTiemposDbContext _unidadDeTrabajo;
            private readonly IManejadorDeCache _cache;

            public Handler(ControlTiemposDbContext unidadDeTrabajo, IManejadorDeCache cache)
            {
                _unidadDeTrabajo = unidadDeTrabajo;
                _cache = cache;
            }

            protected override Task HandleCore(Command message)
            {
                var llave = $"U({message.UsuarioId})P({message.ProyectoId})S({message.ServicioId})A({message.ActividadId})";
                var cronometro = _cache.Obtener<CronometroDTO>(llave);

                if (cronometro == null)
                    cronometro = new CronometroDTO
                    {
                        ActividadId = message.ActividadId,
                        Inicio = FechaSistema.Actual(),
                        ProyectoId = message.ProyectoId,
                        ServicioId = message.ServicioId,
                        UsuarioId = message.UsuarioId
                    };
                else
                    cronometro.Inicio = FechaSistema.Actual();

                _cache.Agregar(llave, cronometro);

                return Task.FromResult<object>(null);
            }
        }
    }
}