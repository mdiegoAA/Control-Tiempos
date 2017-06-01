// ----------------------------------------------------------------------------------------------
// <copyright file="IServicioAreas.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Amezquita.ControlTiempos.Infraestructura;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Areas
{
    public interface IServicioAreas
    {
        #region Instance Methods

        Task CrearAreaAsync(GuardarAreaDTO dto);

        Task EditarAreaAsync(GuardarAreaDTO dto);

        Task EliminarAreaAsync(EliminarAreaDTO dto);

        Task<AreaDTO> ObtenerAreaPorIdAsync(ObtenerAreaPorIdDTO dto);

        Task<Paginado<AreaDTO>> ObtenerAreasPorPaginaAsync(ObtenerAreasPorPaginaDTO paginaDTO);

        #endregion
    }
}