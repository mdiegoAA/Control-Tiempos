// ----------------------------------------------------------------------------------------------
// <copyright file="ICarguesService.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Mobile</project>
// ----------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amezquita.ControlTiempos.Mobile.Features.Cargues
{
    public interface ICarguesService
    {
        #region Instance Methods

        Task<IEnumerable<ActividadDTO>> ObtenerActividadesAsync(ProyectoDTO dto);
        Task<HistorialPaginadoDTO> ObtenerHistorialAsync(int numeroPagina, int registrosPagina, string buscar, bool esApp);
        Task<IEnumerable<ProyectoDTO>> ObtenerProyectosAsync();
        Task<IEnumerable<ServicioDTO>> ObtenerServiciosAsync(ProyectoDTO dto);
        Task<RespuestaRegistrarTiempoDTO> RegistrarTiempoAsync(RegistrarTiempoDTO dto);

        #endregion
    }
}
