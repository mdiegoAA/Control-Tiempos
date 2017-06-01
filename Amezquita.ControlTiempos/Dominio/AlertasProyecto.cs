// ----------------------------------------------------------------------------------------------
// <copyright file="AlertaProyecto.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Dominio</project>
// ----------------------------------------------------------------------------------------------

using System;

namespace Amezquita.ControlTiempos.Dominio
{
    public class AlertasProyecto : Entity<Guid>
    {
        private int _diasSinRegistrar;
        private decimal _porcentajeEjecutado;

        #region C'tors

        public AlertasProyecto(Guid id, decimal porcentajeEjecutado, int diasSinRegistrar)
        {
            Id = id;
            PorcentajeEjecutado = porcentajeEjecutado;
            DiasSinRegistrar = diasSinRegistrar;
        }

        private AlertasProyecto() {}

        #endregion

        #region Instance Properties

        public int DiasSinRegistrar
        {
            get { return _diasSinRegistrar; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(DiasSinRegistrar), "La cantidad de días sin registrar en las alertas del proyecto debe ser mayor que cero.");
                }

                _diasSinRegistrar = value;
            }
        }

        public decimal PorcentajeEjecutado
        {
            get { return _porcentajeEjecutado; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(PorcentajeEjecutado), "El porcentaje ejecutado en las alertas del proyecto debe ser mayor que cero.");
                }

                _porcentajeEjecutado = value;
            }
        }

        #endregion
    }
}