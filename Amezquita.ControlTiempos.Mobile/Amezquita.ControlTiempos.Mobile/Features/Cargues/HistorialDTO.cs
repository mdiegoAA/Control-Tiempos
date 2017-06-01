using System;

namespace Amezquita.ControlTiempos.Mobile.Features.Cargues
{
    public class HistorialDTO
    {
        public string ActividadNombre { get; set; }
        public bool Aprobada { get; set; }
        public string AprobadaTexto { get; set; }
        public bool EsNovedad { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Guid Id { get; set; }
        public string Observacion { get; set; }
        public string ProyectoNombre { get; set; }
        public string ServicioNombre { get; set; }
        public string UsuarioNombre { get; set; }
    }
}