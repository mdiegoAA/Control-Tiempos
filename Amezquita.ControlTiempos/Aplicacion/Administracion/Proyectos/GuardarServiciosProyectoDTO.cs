// ----------------------------------------------------------------------------------------------
// <copyright file="GuardarServiciosProyectoDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Proyectos
{
    public class GuardarServiciosProyectoDTO
    {
        #region Instance Properties

        public Guid ProyectoId { get; set; }

        public IEnumerable<Guid> Servicios { get; set; }

        #endregion
    }
}