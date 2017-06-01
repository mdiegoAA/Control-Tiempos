// ----------------------------------------------------------------------------------------------
// <copyright file="GuardarGrupoDeTrabajoProyectoDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Proyectos
{
    public class GuardarGrupoDeTrabajoProyectoDTO
    {
        #region Instance Properties

        public Guid ProyectoId { get; set; }

        [Required(ErrorMessage = "El id del proyecto es obligatorio.")]
        public IEnumerable<Guid> Usuarios { get; set; }

        #endregion
    }
}