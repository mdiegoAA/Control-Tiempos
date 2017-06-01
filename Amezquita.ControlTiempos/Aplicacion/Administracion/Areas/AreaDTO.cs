// ----------------------------------------------------------------------------------------------
// <copyright file="AreaDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Areas
{
    public class AreaDTO
    {
        #region Instance Properties

        public string Codigo { get; set; }

        public Guid Id { get; set; }

        public string Nombre { get; set; }

        #endregion
    }
}