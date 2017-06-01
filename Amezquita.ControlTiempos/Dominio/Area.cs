// ----------------------------------------------------------------------------------------------
// <copyright file="Area.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Dominio</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Amezquita.ControlTiempos.Dominio
{
    public class Area : Entity<Guid>
    {
        #region Fields

        private HashSet<AreaProyecto> _areasProyecto;
        private string _codigo;
        private string _nombre;

        #endregion

        #region C'tors

        public Area(Guid id, string codigo, string nombre)
        {
            Id = id;
            Codigo = codigo;
            Nombre = nombre;
        }

        private Area() {}

        #endregion

        #region Instance Properties

        public ICollection<AreaProyecto> AreasProyecto
        {
            get { return _areasProyecto ?? (_areasProyecto = new HashSet<AreaProyecto>()); }
            set { _areasProyecto = new HashSet<AreaProyecto>(value); }
        }

        public string Codigo
        {
            get { return _codigo; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(Codigo), "El código del área del proyecto no puede ser nulo o vacío.");
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
                    throw new ArgumentNullException(nameof(Nombre), "El nombre del área del proyecto no puede ser nuvo o vacío.");
                }

                _nombre = value;
            }
        }

        #endregion
    }
}