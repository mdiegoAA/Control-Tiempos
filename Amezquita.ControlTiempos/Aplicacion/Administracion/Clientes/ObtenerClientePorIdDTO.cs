// ----------------------------------------------------------------------------------------------
// <copyright file="ObtenerClientePorIdDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Clientes
{
    public class ObtenerClientePorIdDTO
    {
        #region Instance Properties

        [Required(ErrorMessage = "El id del cliente es obligatorio.")]
        public Guid Id { get; set; }

        #endregion
    }
}