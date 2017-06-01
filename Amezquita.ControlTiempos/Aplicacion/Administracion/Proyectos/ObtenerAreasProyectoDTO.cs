// ----------------------------------------------------------------------------------------------
// <copyright file="ObtenerAreasProyectoDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Proyectos
{
    public class ObtenerAreasProyectoDTO
    {
        #region Instance Properties

        [Required(ErrorMessage = "El id del proyecto es obligatorio.")]
        public Guid Id { get; set; }

        #endregion
    }
}