// ----------------------------------------------------------------------------------------------
// <copyright file="CargueHoras.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Dominio</project>
// ----------------------------------------------------------------------------------------------

using System;
using Amezquita.ControlTiempos.Infraestructura;

namespace Amezquita.ControlTiempos.Dominio
{
    public class CargueHoras : Entity<Guid>
    {
        #region Fields

        private Actividad _actividad;
        private DateTime? _fechaAprobacion;
        private DateTime _fechaFin;
        private DateTime _fechaInicio;
        private DateTime _fechaRegistro;
        private Proyecto _proyecto;
        private Servicio _servicio;
        private Usuario _usuario;
        private Usuario _registradaPor;

        #endregion

        #region C'tors

        public CargueHoras(Guid id, Proyecto proyecto, Usuario usuario, Servicio servicio, Actividad actividad, DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin, string observacion)
        {
            Id = id;
            Proyecto = proyecto;
            Usuario = usuario;
            Servicio = servicio;
            Actividad = actividad;
            Observacion = observacion;
            Aprobada = !actividad.NecesitaAprobacion;
            EsNovedad = false;
            RegistradaPor = usuario;
            FechaRegistro = FechaSistema.Actual();

            EstablecerTiempos(fecha, horaInicio, horaFin);
        }

        public CargueHoras(Guid id, Proyecto proyecto, Usuario usuario, Servicio servicio, Actividad actividad, DateTime fechaInicio, DateTime fechaFin, string observacion, Usuario registradaPor)
        {
            Id = id;
            Proyecto = proyecto;
            Usuario = usuario;
            Servicio = servicio;
            Actividad = actividad;
            Observacion = observacion;
            Aprobada = !actividad.NecesitaAprobacion;
            EsNovedad = true;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            RegistradaPor = registradaPor;
            FechaRegistro = FechaSistema.Actual();
        }

        private CargueHoras() { }

        #endregion

        #region Instance Properties

        public Proyecto Proyecto
        {
            get { return _proyecto; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Proyecto), "El proyecto en el cargue de horas no puede ser nulo.");
                }

                _proyecto = value;
            }
        }

        public Servicio Servicio
        {
            get { return _servicio; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Servicio), "El servicio en el cargue de horas no puede ser nulo.");
                }

                _servicio = value;
            }
        }

        public Actividad Actividad
        {
            get { return _actividad; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Actividad), "La actividad en el cargue de horas no puede ser nulo.");
                }

                _actividad = value;
            }
        }

        public Usuario Usuario
        {
            get { return _usuario; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Usuario), "El usuario en el cargue de horas no puede ser nulo.");
                }

                _usuario = value;
            }
        }

        public decimal HoraFraccion
        {
            get { return (decimal)(FechaFin - FechaInicio).TotalHours; }
        }

        public string Observacion { get; set; }

        public DateTime FechaInicio
        {
            get { return _fechaInicio; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(FechaInicio), "La fecha de inicio en el cargue de horas no puede ser nula.");
                }

                if (value == DateTime.MinValue || value == DateTime.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(FechaInicio), "La fecha de inicio en el cargue de horas está fuera del rango de fecha.");
                }

                _fechaInicio = value;
            }
        }

        public DateTime FechaFin
        {
            get { return _fechaFin; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(FechaFin), "La fecha de fin en el cargue de horas no puede ser nula.");
                }

                if (value == DateTime.MinValue || value == DateTime.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(FechaFin), "La fecha de fin en el cargue de horas está fuera del rango de fecha.");
                }

                _fechaFin = value;
            }
        }

        public DateTime FechaRegistro
        {
            get { return _fechaRegistro; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(FechaRegistro), "La fecha de registro en el cargue de horas no puede ser nula.");
                }

                if (value == DateTime.MinValue || value == DateTime.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(FechaRegistro), "La fecha de registro en el cargue de horas está fuera del rango de fecha.");
                }

                _fechaRegistro = value;
            }
        }

        public Usuario RegistradaPor
        {
            get { return _registradaPor; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Usuario), "El usuario que registra el cargue de horas no puede ser nulo.");
                }

                _registradaPor = value;
            }
        }

        public bool Aprobada { get; private set; }

        public Usuario AprobadaPor { get; private set; }

        public DateTime? FechaAprobacion
        {
            get { return _fechaAprobacion; }
            private set
            {
                if (value.HasValue && (value.Value == DateTime.MinValue || value.Value == DateTime.MaxValue))
                {
                    throw new ArgumentOutOfRangeException(nameof(FechaAprobacion), "La fecha de aprobación en el cargue de horas está fuera del rango de fecha.");
                }

                _fechaAprobacion = value;
            }
        }

        public bool EsNovedad { get; private set; }

        #endregion

        #region Instance Methods

        public void Aprobar(Usuario aprobadaPor)
        {
            Aprobada = true;
            FechaAprobacion = FechaSistema.Actual();
            AprobadaPor = aprobadaPor;
        }

        public void EstablecerTiempos(DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin)
        {
            var fechaSistema = FechaSistema.Actual();

            if (fecha.Date > fechaSistema.Date)
                throw new InvalidOperationException("La fecha de cargue no puede ser mayor a la fecha actual.");

            if (horaFin < horaInicio)
                throw new InvalidOperationException("La hora de fin no puede ser menor a la hora de inicio.");

            var fechaInicio = fecha.Date.Add(horaInicio);
            var fechaFin = fecha.Date.Add(horaFin);
            var diferencia = fechaFin - fechaInicio;

            if (diferencia.TotalMinutes < 15)
                throw new InvalidOperationException("La diferencia entre la fecha de inicio y la fecha final debe ser mayor a 15 minutos.");

            if ((fechaSistema - fecha).TotalDays > Usuario.ReglaCargue.LimiteDias)
                throw new InvalidOperationException($"El usuario no pueden cargar horas al sistemas con más de {Usuario.ReglaCargue.LimiteDias} días de antigüedad.");

            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }

        #endregion
    }
}