// ----------------------------------------------------------------------------------------------
// <copyright file="Proyecto.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Dominio</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Amezquita.ControlTiempos.Dominio
{
    public class Proyecto : Entity<Guid>
    {
        #region Fields

        private HashSet<Actividad> _actividades;
        private int _año;
        private HashSet<AreaProyecto> _areasProyecto;
        private decimal _bolsaHoras;
        private Cliente _cliente;
        private Usuario _director;
        private DateTime _fechaFinal;
        private DateTime _fechaInicio;
        private Usuario _gerente;
        private HashSet<Usuario> _grupoTrabajo;
        private string _nombre;
        private HashSet<Servicio> _servicios;

        #endregion

        #region C'tors

        public Proyecto(Guid id, Cliente cliente, string nombre, int año, DateTime fechaInicio, DateTime fechaFinal, decimal bolsaHoras, Usuario gerente, Usuario director, Usuario supervisor, Usuario auditor)
        {
            Id = id;
            Cliente = cliente;
            Nombre = nombre;
            Año = año;
            FechaInicio = fechaInicio;
            FechaFinal = fechaFinal;
            BolsaHoras = bolsaHoras;
            Gerente = gerente;
            Director = director;
            Supervisor = supervisor;
            Auditor = auditor;
        }

        private Proyecto() {}

        #endregion

        #region Instance Properties
        public Cliente Cliente
        {
            get { return _cliente; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Cliente), "El cliente del proyecto no puede ser nulo.");
                }

                _cliente = value;
            }
        }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(Nombre), "El nombre del proyecto no puede ser nulo o vacío.");
                }

                _nombre = value;
            }
        }

        public int Año
        {
            get { return _año; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Año), "El año del proyecto no puede ser menor o igual a cero.");
                }

                _año = value;
            }
        }

        public DateTime FechaInicio
        {
            get { return _fechaInicio; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(FechaInicio), "La fecha de inicio del proyecto no puede ser nula.");
                }

                if (value == DateTime.MinValue || value == DateTime.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(FechaInicio), "La fecha de inicio del proyecto está fuera del rango de fecha.");
                }

                _fechaInicio = value;
            }
        }

        public DateTime FechaFinal
        {
            get { return _fechaFinal; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(FechaFinal), "La fecha de fin del proyecto no puede ser nula.");
                }

                if (value == DateTime.MinValue || value == DateTime.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(FechaFinal), "La fecha de fin del proyecto está fuera del rango de fecha.");
                }

                _fechaFinal = value;
            }
        }

        public decimal BolsaHoras
        {
            get { return _bolsaHoras; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(BolsaHoras), "La bolsa de horas del proyecto debe ser mayor que cero.");
                }

                _bolsaHoras = value;
            }
        }

        public Usuario Gerente
        {
            get { return _gerente; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Gerente), "El gerente del proyecto no puede ser nulo.");
                }

                _gerente = value;
            }
        }

        public Usuario Director
        {
            get { return _director; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Director), "El director del proyecto no puede ser nulo.");
                }

                _director = value;
            }
        }

        public Usuario Supervisor { get; set; }

        public Usuario Auditor { get; set; }

        public AlertasProyecto AlertasProyecto { get; set; }

        public ICollection<Servicio> Servicios
        {
            get { return _servicios ?? (_servicios = new HashSet<Servicio>()); }
            set { _servicios = new HashSet<Servicio>(value); }
        }

        public ICollection<Actividad> Actividades
        {
            get { return _actividades ?? (_actividades = new HashSet<Actividad>()); }
            set { _actividades = new HashSet<Actividad>(value); }
        }

        public ICollection<Usuario> GrupoTrabajo
        {
            get { return _grupoTrabajo ?? (_grupoTrabajo = new HashSet<Usuario>()); }
            set { _grupoTrabajo = new HashSet<Usuario>(value); }
        }

        public ICollection<AreaProyecto> AreasProyecto
        {
            get { return _areasProyecto ?? (_areasProyecto = new HashSet<AreaProyecto>()); }
            set { _areasProyecto = new HashSet<AreaProyecto>(value); }
        }

        #endregion

        #region Instance Methods

        public bool ActividadAsociada(Guid actividadId)
        {
            return Actividades.Any(a => a.Id == actividadId);
        }

        public void AgregarActividad(Actividad actividad)
        {
            if (actividad == null)
            {
                throw new ArgumentNullException(nameof(actividad));
            }

            Actividades.Add(actividad);
        }

        public void AgregarServicio(Servicio servicio)
        {
            if (servicio == null)
            {
                throw new ArgumentNullException(nameof(servicio));
            }

            Servicios.Add(servicio);
        }

        public void AgregarUsuarioAlGrupoDeTrabajo(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            GrupoTrabajo.Add(usuario);
        }

        public bool ServicioAsociado(Guid servicioId)
        {
            return Servicios.Any(s => s.Id == servicioId);
        }

        public bool UsuarioEsAdministrador(Guid usuarioId)
        {
            return (Auditor != null && Auditor.Id == usuarioId) ||
                   (Director != null && Director.Id == usuarioId) ||
                   (Gerente != null && Gerente.Id == usuarioId) ||
                   (Supervisor != null && Supervisor.Id == usuarioId);
        }

        public bool UsuarioPerteneceAlGrupo(Guid usuarioId)
        {
            return GrupoTrabajo.Any(u => u.Id == usuarioId);
        }

        #endregion
    }
}