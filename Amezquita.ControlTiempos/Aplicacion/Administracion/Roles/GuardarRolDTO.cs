// ----------------------------------------------------------------------------------------------
// <copyright file="GuardarRolDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Roles
{
    public class GuardarRolDTO
    {
        #region Instance Properties

        [Required(ErrorMessage = "El id del rol es obligatorio.")]
        [Display(Name = "Id")]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        #endregion
    }
}