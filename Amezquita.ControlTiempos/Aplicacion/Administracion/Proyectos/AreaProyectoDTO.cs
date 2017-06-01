// ----------------------------------------------------------------------------------------------
// <copyright file="AreaProyectoDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Proyectos
{
    public class AreaProyectoDTO
    {
        #region Instance Properties

        public Guid AreaId { get; set; }

        public string AreaNombre { get; set; }

        public int Horas { get; set; }

        #endregion
    }
}