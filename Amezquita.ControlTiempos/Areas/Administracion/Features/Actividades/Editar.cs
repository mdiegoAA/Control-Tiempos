using Amezquita.ControlTiempos.Infraestructura.Datos;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Amezquita.ControlTiempos.Areas.Administracion.Features.Actividades
{
    public class Editar
    {
        public class Command : IAsyncRequest
        {
            [Required(ErrorMessage = "El código de la actividad es obligatorio.")]
            [Display(Name = "Código")]
            public string Codigo { get; set; }

            [Display(Name = "Es Genérica")]
            public bool EsGenerica { get; set; }

            [Required(ErrorMessage = "El id de la actividad es obligatorio.")]
            [Display(Name = "Id")]
            public Guid Id { get; set; }

            [Display(Name = "Necesita Aprobación")]
            public bool NecesitaAprobacion { get; set; }

            [Required(ErrorMessage = "El nombre de la actividad es obligatorio.")]
            [Display(Name = "Nombre")]
            public string Nombre { get; set; }
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

                if (string.IsNullOrEmpty(message.Codigo))
                    throw new ArgumentException("El código de la actividad es obligatorio.");

                if (string.IsNullOrEmpty(message.Nombre))
                    throw new ArgumentException("El nombre de la actividad es obligatorio.");

                var actividad = await _unidadDeTrabajo.Actividades.FirstOrDefaultAsync(x => x.Id == message.Id);

                if (actividad == null)
                    throw new ArgumentException("La actividad a editar no existe o no se encuentra registrada.");

                actividad.Codigo = message.Codigo;
                actividad.Nombre = message.Nombre;
                actividad.EsGenerica = message.EsGenerica;
                actividad.NecesitaAprobacion = message.NecesitaAprobacion;

                await _unidadDeTrabajo.SaveChangesAsync();
            }
        }
    }
}
