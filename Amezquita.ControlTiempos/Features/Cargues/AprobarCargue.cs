using Amezquita.ControlTiempos.Infraestructura;
using Amezquita.ControlTiempos.Infraestructura.Datos;
using MediatR;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Amezquita.ControlTiempos.Features.Cargues
{
    public class AprobarCargue
    {
        public class Command : IAsyncRequest
        {
            public Guid Id { get; set; }

            [PropertyModelBinder(typeof(UserIdModelBinder))]
            public Guid UsuarioId { get; set; }
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
                var cargueHoras = await _unidadDeTrabajo.CarguesHoras.FirstOrDefaultAsync(ch => ch.Id == message.Id);

                if (cargueHoras == null)
                    throw new InvalidOperationException("El cargue de horas no existe o no se encuentra registrado.");

                var usuario = await _unidadDeTrabajo.Usuarios.FirstOrDefaultAsync(u => u.Id == message.UsuarioId);

                if (usuario == null)
                    throw new InvalidOperationException("El usuario existe o no se encuentra registrado.");

                cargueHoras.Aprobar(usuario);

                await _unidadDeTrabajo.SaveChangesAsync();
            }
        }
    }
}