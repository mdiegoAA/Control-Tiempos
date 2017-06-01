using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amezquita.ControlTiempos.Mobile.Features.Cargues
{
    public class RegistrarTiempoDTO
    {
        public Guid ActividadId { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraFin { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public string Observacion { get; set; }
        public Guid ProyectoId { get; set; }
        public Guid ServicioId { get; set; }
    }
}