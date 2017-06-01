// ----------------------------------------------------------------------------------------------
// <copyright file="ObtenerAreaPorIdDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Areas
{
    public class ObtenerAreaPorIdDTO
    {
        #region Instance Properties

        [Required(ErrorMessage = "El id del área es obligatorio.")]
        public Guid Id { get; set; }

        #endregion
    }
}