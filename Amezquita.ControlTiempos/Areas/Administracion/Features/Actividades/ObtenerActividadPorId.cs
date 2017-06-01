using Amezquita.ControlTiempos.Infraestructura.Datos;
using Amezquita.ControlTiempos.Infraestructura.Extensiones;
using MediatR;
using Omu.ValueInjecter;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Amezquita.ControlTiempos.Areas.Administracion.Features.Actividades
{
    public class ObtenerActividadPorId
    {
        public class Query : IAsyncRequest<ActividadDTO>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Query, ActividadDTO>
        {
            private readonly ControlTiemposDbContext _unidadDeTrabajo;

            public Handler(ControlTiemposDbContext unidadDeTrabajo)
            {
                _unidadDeTrabajo = unidadDeTrabajo;
            }

            public async Task<ActividadDTO> Handle(Query message)
            {
                if (message == null)
                    throw new ArgumentNullException(nameof(message));

                if (message.Id == Guid.Empty)
                    throw new ArgumentException("El id de la actividad es obligatorio.");

                var actividad = await _unidadDeTrabajo.Actividades.FirstOrDefaultAsync(x => x.Id == message.Id);

                if (actividad == null)
                    return null;

                var dto = new ActividadDTO();
                dto.InjectFrom(actividad);
                return dto;
            }
        }
    }
}