// ----------------------------------------------------------------------------------------------
// <copyright file="GuardarActividadesProyectoDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Proyectos
{
    public class GuardarActividadesProyectoDTO
    {
        #region Instance Properties

        public IEnumerable<Guid> Actividades { get; set; }

        [Required(ErrorMessage = "El id del proyecto es obligatorio.")]
        public Guid ProyectoId { get; set; }

        #endregion
    }
}