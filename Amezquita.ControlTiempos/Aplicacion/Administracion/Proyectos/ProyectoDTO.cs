// ----------------------------------------------------------------------------------------------
// <copyright file="ProyectoDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Proyectos
{
    public class ProyectoDTO
    {
        #region Instance Properties

        public int AlertasProyectoDiasSinRegistrar { get; set; }

        public decimal AlertasProyectoPorcentajeEjecutado { get; set; }

        public int Año { get; set; }

        public Guid AuditorId { get; set; }

        public string AuditorNombre { get; set; }

        public decimal BolsaHoras { get; set; }

        public Guid ClienteId { get; set; }

        public string ClienteNombre { get; set; }

        public Guid DirectorId { get; set; }

        public string DirectorNombre { get; set; }

        public DateTime FechaFinal { get; set; }

        public DateTime FechaInicio { get; set; }

        public Guid GerenteId { get; set; }

        public string GerenteNombre { get; set; }

        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public Guid SupervisorId { get; set; }

        public string SupervisorNombre { get; set; }

        #endregion
    }
}