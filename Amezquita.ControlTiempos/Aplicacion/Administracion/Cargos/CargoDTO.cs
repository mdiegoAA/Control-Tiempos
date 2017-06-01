// ----------------------------------------------------------------------------------------------
// <copyright file="CargoDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Cargos
{
    public class CargoDTO
    {
        #region Instance Properties

        public string Codigo { get; set; }

        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public decimal Tarifa { get; set; }

        #endregion
    }
}