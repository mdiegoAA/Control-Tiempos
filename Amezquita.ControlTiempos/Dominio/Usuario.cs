// ----------------------------------------------------------------------------------------------
// <copyright file="Usuario.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Dominio</project>
// ----------------------------------------------------------------------------------------------

using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;

namespace Amezquita.ControlTiempos.Dominio
{
    public class Usuario : Entity<Guid>, IUser<Guid>
    {
        #region Fields

        private int _accesosFallidos;
        private Cargo _cargo;
        private string _email;
        private DateTime? _fechaBloqueo;
        private string _nombre;
        private string _cedula;
        private string _userName;
        private HashSet<Rol> _roles;

        #endregion

        #region C'tors

        public Usuario(Guid id, Cargo cargo, string userName, string nombre, string email, string cedula)
        {
            Id = id;
            Cargo = cargo;
            UserName = userName;
            Nombre = nombre;
            Email = email;
            Cedula = cedula;

        }

        public Usuario(Guid id, Cargo cargo, string userName, string nombre, string email, int limiteDias, decimal limiteHoraFraccion, string Cedula)
            : this(id, cargo, userName, nombre, email, Cedula)
        {
            ReglaCargue = new ReglaCargue(id, limiteDias, limiteHoraFraccion);
        }

        private Usuario() {}

        #endregion

        #region Instance Properties




        public string UserName
        {
            get { return _userName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(UserName), "El nombre de usuario no puede ser nulo o vacío.");
                }

                _userName = value;
            }
        }


        public string Cedula
        {
            get { return _cedula; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(UserName), "La Cedula del usuario no puede ser nulo o vacío.");
                }

                _cedula = value;
            }
        }


        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(Nombre), "El nombre del usuario no puede ser nulo o vacío.");
                }

                _nombre = value;
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(Email), "El email del usuario no puede ser nulo o vacío.");
                }

                _email = value;
            }
        }

        public bool EmailConfirmado { get; set; }

        public int AccesosFallidos
        {
            get { return _accesosFallidos; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(AccesosFallidos), "La cantidad de accesos fallidos del usuario debe ser mayor que cero.");
                }

                _accesosFallidos = value;
            }
        }

        public bool Bloqueado { get; set; }

        public DateTime? FechaBloqueo
        {
            get { return _fechaBloqueo; }
            set
            {
                if (value.HasValue && (value.Value == DateTime.MinValue || value.Value == DateTime.MaxValue))
                {
                    throw new ArgumentOutOfRangeException(nameof(FechaBloqueo), "La fecha de bloquedo del usuario está fuera del rango de fecha.");
                }

                _fechaBloqueo = value;
            }
        }

        public Cargo Cargo
        {
            get { return _cargo; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Cargo), "El cargo del usuario no puede ser nulo.");
                }

                _cargo = value;
            }
        }

        public ReglaCargue ReglaCargue { get; set; }

        public ICollection<Rol> Roles
        {
            get { return _roles ?? (_roles = new HashSet<Rol>()); }
            set { _roles = new HashSet<Rol>(value); }
        }

        #endregion
    }
}