// ----------------------------------------------------------------------------------------------
// <copyright file="RegistrarTiempoViewModel.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Mobile</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amezquita.ControlTiempos.Mobile.Infrastructure;
using Xamarin.Forms;

namespace Amezquita.ControlTiempos.Mobile.Features.Cargues
{
    public class RegistrarTiempoViewModel : ObservableBase
    {
        #region Readonly & Static Fields

        private readonly INavigation _navigation;
        private readonly ICarguesService _service;

        #endregion

        #region Fields

        private ActividadDTO _actividad;

        private IList<ActividadDTO> _actividades;

        private DateTime _fecha;

        private TimeSpan _horaFin;

        private TimeSpan _horaInicio;

        private string _observacion;

        private ProyectoDTO _proyecto;

        private IList<ProyectoDTO> _proyectos;

        private Command _registrarTiempo;

        private ServicioDTO _servicio;

        private IList<ServicioDTO> _servicios;

        #endregion

        #region C'tors

        public RegistrarTiempoViewModel(INavigation navigation, ICarguesService service)
        {
            if (navigation == null)
            {
                throw new ArgumentNullException(nameof(navigation));
            }

            _navigation = navigation;

            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            _service = service;

            Fecha = DateTime.Now;
            HoraInicio = DateTime.Now.TimeOfDay;
            HoraFin = DateTime.Now.TimeOfDay;

            Loaded += async (sender, e) =>
                            {
                                var proyectos = await _service.ObtenerProyectosAsync();
                                Proyectos = new List<ProyectoDTO>(proyectos);
                            };

            ProyectoSeleccionado += async (sender, e) =>
                                          {
                                              var serviciosTask = _service.ObtenerServiciosAsync(Proyecto);
                                              var actividadesTask = _service.ObtenerActividadesAsync(Proyecto);

                                              await Task.WhenAll(serviciosTask, actividadesTask);

                                              Servicios = new List<ServicioDTO>(await serviciosTask);
                                              Actividades = new List<ActividadDTO>(await actividadesTask);
                                          };

            Loaded?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Instance Properties

        public ActividadDTO Actividad
        {
            get { return _actividad; }
            set
            {
                if (SetProperty(ref _actividad, value))
                {
                    RegistrarTiempo.ChangeCanExecute();
                }
            }
        }

        public IList<ActividadDTO> Actividades
        {
            get { return _actividades; }
            set { SetProperty(ref _actividades, value); }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
            set { SetProperty(ref _fecha, value); }
        }

        public TimeSpan HoraFin
        {
            get { return _horaFin; }
            set { SetProperty(ref _horaFin, value); }
        }

        public TimeSpan HoraInicio
        {
            get { return _horaInicio; }
            set { SetProperty(ref _horaInicio, value); }
        }

        public string Observacion
        {
            get { return _observacion; }
            set { SetProperty(ref _observacion, value); }
        }

        public ProyectoDTO Proyecto
        {
            get { return _proyecto; }
            set
            {
                if (SetProperty(ref _proyecto, value))
                {
                    ProyectoSeleccionado?.Invoke(this, EventArgs.Empty);
                    RegistrarTiempo.ChangeCanExecute();
                }
            }
        }

        public IList<ProyectoDTO> Proyectos
        {
            get { return _proyectos; }
            set { SetProperty(ref _proyectos, value); }
        }

        public Command RegistrarTiempo => _registrarTiempo ?? (_registrarTiempo = new Command(async () =>
                                                                                                    {
                                                                                                        var dto = new RegistrarTiempoDTO
                                                                                                                  {
                                                                                                                      ActividadId = Actividad.Id,
                                                                                                                      Fecha = Fecha,
                                                                                                                      HoraFin = HoraFin,
                                                                                                                      HoraInicio = HoraInicio,
                                                                                                                      Observacion = Observacion,
                                                                                                                      ProyectoId = Proyecto.Id,
                                                                                                                      ServicioId = Servicio.Id
                                                                                                                  };

                                                                                                        var result = await _service.RegistrarTiempoAsync(dto);

                                                                                                        var page = new ContentPage();
                                                                                                        await page.DisplayAlert("Alerta", result == null ? "Registro de historial cargado exitosamente." : result.exceptionMessage, "OK");
                                                                                                    }, PuedeRegistrar));

        public ServicioDTO Servicio
        {
            get { return _servicio; }
            set
            {
                if (SetProperty(ref _servicio, value))
                {
                    RegistrarTiempo.ChangeCanExecute();
                }
            }
        }

        public IList<ServicioDTO> Servicios
        {
            get { return _servicios; }
            set { SetProperty(ref _servicios, value); }
        }

        #endregion

        #region Instance Methods

        private bool PuedeRegistrar()
        {
            if (Proyecto == null)
            {
                return false;
            }
            if (Servicio == null)
            {
                return false;
            }
            if (Actividad == null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Event Declarations

        private event EventHandler Loaded;
        private event EventHandler ProyectoSeleccionado;

        #endregion
    }
}
