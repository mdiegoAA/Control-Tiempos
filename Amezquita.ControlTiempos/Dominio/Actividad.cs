// ----------------------------------------------------------------------------------------------
// <copyright file="Actividad.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Dominio</project>
// ----------------------------------------------------------------------------------------------

using System;

namespace Amezquita.ControlTiempos.Dominio
{
    public class Actividad : Entity<Guid>
    {
        #region Fields

        private string _codigo;
        private string _nombre;

        #endregion

        #region C'tors

        public Actividad(Guid id, string codigo, string nombre, bool esGenerica, bool necesitaAprobacion)
        {
            Id = id;
            Codigo = codigo;
            Nombre = nombre;
            EsGenerica = esGenerica;
            NecesitaAprobacion = necesitaAprobacion;
        }

        private Actividad() {}

        #endregion

        #region Instance Properties

        public string Codigo
        {
            get { return _codigo; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(Codigo), "El código de la actividad no puede nulo o vacío.");
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
                    throw new ArgumentNullException(nameof(Nombre), "El nombre de la actividad no puede ser nulo o vacío.");
                }

                _nombre = value;
            }
        }

        public bool EsGenerica { get; set; }

        public bool NecesitaAprobacion { get; set; }

        #endregion
    }
}