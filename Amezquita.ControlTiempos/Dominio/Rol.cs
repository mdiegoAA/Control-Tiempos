// ----------------------------------------------------------------------------------------------
// <copyright file="Rol.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Dominio</project>
// ----------------------------------------------------------------------------------------------

using Microsoft.AspNet.Identity;
using System;

namespace Amezquita.ControlTiempos.Dominio
{
    public class Rol : Entity<Guid>, IRole<Guid>
    {
        #region Fields

        private string _name;

        #endregion

        #region C'tors

        public Rol(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        private Rol() {}

        #endregion

        #region Instance Properties

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(Name), "El nombre del rol no puede ser nulo o vacío.");
                }

                _name = value;
            }
        }

        #endregion
    }
}