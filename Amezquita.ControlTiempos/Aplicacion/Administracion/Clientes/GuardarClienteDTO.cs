// ----------------------------------------------------------------------------------------------
// <copyright file="GuardarClienteDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Clientes
{
    public class GuardarClienteDTO
    {
        #region Instance Properties

        [Required(ErrorMessage = "El código del cliente es obligatorio.")]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El id del cliente es obligatorio.")]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El NIT del cliente es obligatorio.")]
        [Display(Name = "NIT")]
        public string NIT { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        #endregion
    }
}