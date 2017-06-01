// ----------------------------------------------------------------------------------------------
// <copyright file="DiaCalendario.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Dominio</project>
// ----------------------------------------------------------------------------------------------

using System;

namespace Amezquita.ControlTiempos.Dominio
{
    public class DiaCalendario : Entity<Guid>
    {
        #region C'tors

        public DiaCalendario(Guid id, DateTime dia, bool esFestivo)
        {
            Id = id;
            Dia = dia;
            EsFestivo = esFestivo;
        }

        private DiaCalendario() {}

        #endregion

        #region Instance Properties

        public DateTime Dia { get; set; }

        public bool EsFestivo { get; set; }

        #endregion
    }
}