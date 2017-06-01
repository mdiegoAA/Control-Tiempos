// ----------------------------------------------------------------------------------------------
// <copyright file="CarguesService.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Mobile</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Amezquita.ControlTiempos.Mobile.Infrastructure;
using Newtonsoft.Json;

namespace Amezquita.ControlTiempos.Mobile.Features.Cargues
{
    public class CarguesService : ICarguesService
    {
        #region Readonly & Static Fields

        private readonly AuthenticationHeaderValue _auth;
        private readonly IHttpService _http;
        private readonly IStorage _storage;

        #endregion

        #region C'tors

        public CarguesService(IHttpService http, AccessToken accessToken, IStorage storage)
        {
            if (http == null)
            {
                throw new ArgumentNullException(nameof(http));
            }

            _http = http;

            if (accessToken == null)
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            _auth = new AuthenticationHeaderValue(accessToken.Type, accessToken.Token);

            if (storage == null)
            {
                throw new ArgumentNullException(nameof(storage));
            }

            _storage = storage;
        }

        #endregion

        #region Instance Methods

        public async Task<IEnumerable<ActividadDTO>> ObtenerActividadesAsync(ProyectoDTO dto)
        {
            return await _http.GetAsync<IEnumerable<ActividadDTO>>($"/Cargues/ObtenerActividades/{dto.Id}", _auth);
        }

        public async Task<IEnumerable<ProyectoDTO>> ObtenerProyectosAsync()
        {
            return await _http.GetAsync<IEnumerable<ProyectoDTO>>("/Cargues/ObtenerProyectos", _auth);
        }

        public async Task<IEnumerable<ServicioDTO>> ObtenerServiciosAsync(ProyectoDTO dto)
        {
            return await _http.GetAsync<IEnumerable<ServicioDTO>>($"/Cargues/ObtenerServicios/{dto.Id}", _auth);
        }

        public async Task<HistorialPaginadoDTO> ObtenerHistorialAsync(int numeroPagina, int registrosPagina, string buscar, bool esApp)
        {
            return await _http.GetAsync<HistorialPaginadoDTO>($"/Cargues/ObtenerHistorial?buscar={buscar}&numeroPagina={numeroPagina}&registrosPagina={registrosPagina}&esApp={esApp}", _auth);
        }

        public async Task<RespuestaRegistrarTiempoDTO> RegistrarTiempoAsync(RegistrarTiempoDTO dto)
        {
            var json = JsonConvert.SerializeObject(dto);

            return await _http.PostAsync<RespuestaRegistrarTiempoDTO>("/Cargues/CargarHoras",
                new StringContent(json, Encoding.UTF8, "application/json"),
                _auth);
        }

        #endregion
    }
}
