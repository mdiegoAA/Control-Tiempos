using System;

namespace Amezquita.ControlTiempos.Areas.Administracion.Features.Actividades
{
    public class ActividadDTO
    {
        public string Codigo { get; set; }
        public bool EsGenerica { get; set; }
        public Guid Id { get; set; }
        public bool NecesitaAprobacion { get; set; }
        public string Nombre { get; set; }
    }
}