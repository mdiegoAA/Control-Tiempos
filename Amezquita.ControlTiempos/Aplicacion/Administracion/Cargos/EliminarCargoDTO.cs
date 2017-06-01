// ----------------------------------------------------------------------------------------------
// <copyright file="EliminarCargoDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Cargos
{
    public class EliminarCargoDTO
    {
        #region Instance Properties

        [Required(ErrorMessage = "El id del cargo es obligatorio.")]
        public Guid Id { get; set; }

        #endregion
    }
}