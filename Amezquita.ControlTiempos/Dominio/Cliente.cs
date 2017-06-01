// ----------------------------------------------------------------------------------------------
// <copyright file="Cliente.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Dominio</project>
// ----------------------------------------------------------------------------------------------

using System;

namespace Amezquita.ControlTiempos.Dominio
{
    public class Cliente : Entity<Guid>
    {
        #region Fields

        private string _codigo;
        private string _nit;
        private string _nombre;

        #endregion

        #region C'tors

        public Cliente(Guid id, string codigo, string nit, string nombre)
        {
            Id = id;
            Codigo = codigo;
            NIT = nit;
            Nombre = nombre;
        }

        private Cliente() {}

        #endregion

        #region Instance Properties

        public string Codigo
        {
            get { return _codigo; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(Codigo), "El código del cliente no puede ser nulo o vacío.");
                }

                _codigo = value;
            }
        }

        public string NIT
        {
            get { return _nit; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(NIT), "El NIT del cliente no puede ser nulo o vacío.");
                }

                _nit = value;
            }
        }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(Nombre), "El nombre del cliente no puede ser nulo o vacío.");
                }

                _nombre = value;
            }
        }

        #endregion
    }
}