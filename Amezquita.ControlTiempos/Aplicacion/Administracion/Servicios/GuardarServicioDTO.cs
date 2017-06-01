// ----------------------------------------------------------------------------------------------
// <copyright file="GuardarServicioDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Servicios
{
    public class GuardarServicioDTO
    {
        #region Instance Properties

        [Required(ErrorMessage = "El código del servicio es obligatorio.")]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Display(Name = "Es Genérico")]
        public bool EsGenerico { get; set; }

        [Required(ErrorMessage = "El id del servicio es obligatorio.")]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre del servicio es obligatorio.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        #endregion
    }
}