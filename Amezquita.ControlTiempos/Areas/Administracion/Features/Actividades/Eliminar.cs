using Amezquita.ControlTiempos.Infraestructura.Datos;
using MediatR;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Amezquita.ControlTiempos.Areas.Administracion.Features.Actividades
{
    public class Eliminar
    {
        public class Command : IAsyncRequest
        {
            public Guid Id { get; set; }
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
                if (message == null)
                    throw new ArgumentNullException(nameof(message));

                if (message.Id == Guid.Empty)
                    throw new ArgumentException("El id de la actividad es obligatorio.");

                var relacionada = _unidadDeTrabajo.Proyectos
                                                  .Any(t => t.Actividades.Any(a => a.Id == message.Id));

                if (relacionada)
                    throw new ArgumentException("No es posible eliminar la actividad ya que se encuentra asociada a uno o más proyectos.");

                var actividad = await _unidadDeTrabajo.Actividades.FirstOrDefaultAsync(x => x.Id == message.Id);

                if (actividad == null)
                    throw new ArgumentException("La actividad a eliminar no existe o no se encuentra registrada.");

                _unidadDeTrabajo.Actividades.Remove(actividad);

                await _unidadDeTrabajo.SaveChangesAsync();
            }
        }
    }
}