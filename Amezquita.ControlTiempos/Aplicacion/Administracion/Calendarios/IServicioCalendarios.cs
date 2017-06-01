// ----------------------------------------------------------------------------------------------
// <copyright file="IServicioCalendarios.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Amezquita.ControlTiempos.Infraestructura;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Calendarios
{
    public interface IServicioCalendarios
    {
        #region Instance Methods

        Task CrearDiaCalendarioAsync(GuardarDiaCalendarioDTO dto);

        Task EditarDiaCalendarioAsync(GuardarDiaCalendarioDTO dto);

        Task EliminarDiaCalendarioAsync(EliminarDiaCalendarioDTO dto);

        Task<DiaCalendarioDTO> ObtenerDiaCalendarioAsync(ObtenerDiaCalendarioDTO dto);

        Task<Paginado<DiaCalendarioDTO>> ObtenerDiasCalendarioPorPaginasAsync(ObtenerDiasCalendarioPorPaginaDTO paginaDTO);

        #endregion
    }
}