// ----------------------------------------------------------------------------------------------
// <copyright file="HistorialViewModel.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Mobile</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Amezquita.ControlTiempos.Mobile.Infrastructure;

namespace Amezquita.ControlTiempos.Mobile.Features.Cargues
{
    public class HistorialViewModel : ObservableBase
    {
        #region Readonly & Static Fields

        private readonly ICarguesService _service;

        #endregion

        #region Fields

        private IList<HistorialDTO> _historial;

        #endregion

        #region C'tors

        public HistorialViewModel(ICarguesService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            _service = service;

            Loaded += async (sender, e) =>
                            {
                                var historial = await _service.ObtenerHistorialAsync(1, 1000, "", true);
                                Historial = new List<HistorialDTO>(historial.Datos.Select(h => new HistorialDTO
                                                                                               {
                                                                                                   Id = h.Id,
                                                                                                   ActividadNombre = h.ActividadNombre,
                                                                                                   Aprobada = h.Aprobada,
                                                                                                   AprobadaTexto = h.Aprobada ? "Si" : "No",
                                                                                                   EsNovedad = h.EsNovedad,
                                                                                                   FechaFin = h.FechaFin,
                                                                                                   FechaInicio = h.FechaInicio,
                                                                                                   FechaRegistro = h.FechaRegistro,
                                                                                                   Observacion = h.Observacion,
                                                                                                   ProyectoNombre = h.ProyectoNombre,
                                                                                                   ServicioNombre = h.ServicioNombre,
                                                                                                   UsuarioNombre = h.UsuarioNombre
                                                                                               }));
                            };

            Loaded?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Instance Properties

        public IList<HistorialDTO> Historial
        {
            get { return _historial; }
            set { SetProperty(ref _historial, value); }
        }

        #endregion

        #region Event Declarations

        private event EventHandler Loaded;

        #endregion
    }
}
