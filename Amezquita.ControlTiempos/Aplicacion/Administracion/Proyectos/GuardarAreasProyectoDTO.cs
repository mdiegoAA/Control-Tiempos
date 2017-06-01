// ----------------------------------------------------------------------------------------------
// <copyright file="GuardarAreasProyectoDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Proyectos
{
    public class GuardarAreasProyectoDTO
    {
        #region Instance Properties

        public IEnumerable<AreaProyectoDTO> Areas { get; set; }

        [Required(ErrorMessage = "El id del proyecto es obligatorio.")]
        public Guid ProyectoId { get; set; }

        #endregion
    }
}