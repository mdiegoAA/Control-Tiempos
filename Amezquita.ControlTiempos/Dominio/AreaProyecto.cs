// ----------------------------------------------------------------------------------------------
// <copyright file="AreaProyecto.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Dominio</project>
// ----------------------------------------------------------------------------------------------

using System;

namespace Amezquita.ControlTiempos.Dominio
{
    public class AreaProyecto : Entity<Guid>
    {
        #region Instance Properties

        private AreaProyecto() {}

        public AreaProyecto(Guid id, Guid proyectoId, Guid areaId, int horas)
        {
            Id = id;
            ProyectoId = proyectoId;
            AreaId = areaId;
            Horas = horas;
        }

        public Proyecto Proyecto { get; set; }

        public Guid ProyectoId { get; set; }

        public Area Area { get; set; }

        public Guid AreaId { get; set; }

        public int Horas { get; set; }

        #endregion
    }
}