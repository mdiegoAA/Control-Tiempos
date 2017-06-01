// ----------------------------------------------------------------------------------------------
// <copyright file="Servicio.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Dominio</project>
// ----------------------------------------------------------------------------------------------

using System;

namespace Amezquita.ControlTiempos.Dominio
{
    public class Servicio : Entity<Guid>
    {
        #region Fields

        private string _codigo;
        private string _nombre;

        #endregion

        #region C'tors

        public Servicio(Guid id, string codigo, string nombre, bool esGenerico)
        {
            Id = id;
            Codigo = codigo;
            Nombre = nombre;
            EsGenerico = esGenerico;
        }

        private Servicio() {}

        #endregion

        #region Instance Properties

        public string Codigo
        {
            get { return _codigo; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(Codigo), "El código del servicio no puede ser nulo o vacío.");
                }

                _codigo = value;
            }
        }

        public bool EsGenerico { get; set; }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(Nombre), "El nombre del servicio no puede ser nulo o vacío.");
                }

                _nombre = value;
            }
        }

        #endregion
    }
}