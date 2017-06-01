// ----------------------------------------------------------------------------------------------
// <copyright file="ObtenerProyectoPorIdDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Proyectos
{
    public class ObtenerProyectoPorIdDTO
    {
        #region Instance Properties

        [Required(ErrorMessage = "El id del proyecto es obligatorio.")]
        public Guid Id { get; set; }

        #endregion
    }
}