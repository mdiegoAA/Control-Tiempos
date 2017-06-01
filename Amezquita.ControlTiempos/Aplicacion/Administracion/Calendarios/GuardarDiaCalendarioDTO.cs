// ----------------------------------------------------------------------------------------------
// <copyright file="GuardarDiaCalendarioDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Calendarios
{
    public class GuardarDiaCalendarioDTO
    {
        #region Instance Properties

        [Required(ErrorMessage = "El día calendario es obligatorio.")]
        [Display(Name = "Día")]
        public DateTime Dia { get; set; }

        [Required(ErrorMessage = "Indique si el día calendario es festivo o no.")]
        [Display(Name = "¿Es Festivo?")]
        public bool EsFestivo { get; set; }

        [Required(ErrorMessage = "El id del día calendario es obligatorio.")]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        #endregion
    }
}