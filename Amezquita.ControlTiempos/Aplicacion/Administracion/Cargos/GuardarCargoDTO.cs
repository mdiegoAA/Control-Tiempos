// ----------------------------------------------------------------------------------------------
// <copyright file="GuardarCargoDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Cargos
{
    public class GuardarCargoDTO
    {
        #region Instance Properties

        [Required(ErrorMessage = "El código del cargo es obligatorio.")]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El id del cargo es obligatorio.")]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre del cargo es obligatorio.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El valor de la tarifa debe ser mayor que {1}")]
        [Display(Name = "Tarifa")]
        [Numeric(ErrorMessage = "La tarifa debe ser un número decimal válido.")]
        public decimal Tarifa { get; set; }

        #endregion
    }
}