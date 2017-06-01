// ----------------------------------------------------------------------------------------------
// <copyright file="ReglaCargue.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Dominio</project>
// ----------------------------------------------------------------------------------------------

using System;

namespace Amezquita.ControlTiempos.Dominio
{
    public class ReglaCargue : Entity<Guid>
    {
        #region Fields

        private int _limiteDias;
        private decimal _limiteHoraFraccion;

        #endregion

        #region C'tors

        public ReglaCargue(Guid id, int limiteDias, decimal limiteHoraFraccion)
        {
            Id = id;
            LimiteDias = limiteDias;
            LimiteHoraFraccion = limiteHoraFraccion;
        }

        private ReglaCargue() {}

        #endregion

        #region Instance Properties

        public int LimiteDias
        {
            get { return _limiteDias; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(LimiteDias), "El límite de días de la regla de cargue debe ser mayor que cero.");
                }

                _limiteDias = value;
            }
        }

        public decimal LimiteHoraFraccion
        {
            get { return _limiteHoraFraccion; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(LimiteHoraFraccion), "El límite de hora o fracción de la regla de cargue debe ser mayor que cero.");
                }

                _limiteHoraFraccion = value;
            }
        }

        #endregion
    }
}