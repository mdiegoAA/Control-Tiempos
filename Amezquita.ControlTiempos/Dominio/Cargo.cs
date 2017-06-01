// ----------------------------------------------------------------------------------------------
// <copyright file="Cargo.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Dominio</project>
// ----------------------------------------------------------------------------------------------

using System;

namespace Amezquita.ControlTiempos.Dominio
{
    public class Cargo : Entity<Guid>
    {
        #region Fields

        private string _codigo;
        private string _nombre;
        private decimal _tarifa;

        #endregion

        #region C'tors

        public Cargo(Guid id, string codigo, string nombre, decimal tarifa)
        {
            Id = id;
            Codigo = codigo;
            Nombre = nombre;
            Tarifa = tarifa;
        }

        private Cargo() {}

        #endregion

        #region Instance Properties

        public string Codigo
        {
            get { return _codigo; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(Codigo), "El código del cargo no puede ser nulo o vacío.");
                }

                _codigo = value;
            }
        }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(Nombre), "El nombre del cargo no puede ser nulo o vacío.");
                }

                _nombre = value;
            }
        }

        public decimal Tarifa
        {
            get { return _tarifa; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Tarifa), "El valor de la tarifa del cargo no puede ser menor que cero.");
                }

                _tarifa = value;
            }
        }

        #endregion
    }
}