// ----------------------------------------------------------------------------------------------
// <copyright file="DiaCalendarioDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Calendarios
{
    public class DiaCalendarioDTO
    {
            #region Instance Properties

        public DateTime Dia { get; set; }

        public bool EsFestivo { get; set; }

        public Guid Id { get; set; }

        #endregion
    }
}