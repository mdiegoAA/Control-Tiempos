// ----------------------------------------------------------------------------------------------
// <copyright file="IServicioCargos.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Amezquita.ControlTiempos.Infraestructura;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Cargos
{
    public interface IServicioCargos
    {
        #region Instance Methods

        Task CrearCargoAsync(GuardarCargoDTO dto);

        Task EditarCargoAsync(GuardarCargoDTO dto);

        Task EliminarCargoAsync(EliminarCargoDTO dto);

        Task<CargoDTO> ObtenerCargosPorIdAsync(ObtenerCargoPorIdDTO dto);

        Task<Paginado<CargoDTO>> ObtenerCargosPorPaginaAsync(ObtenerCargosPorPaginaDTO paginaDTO);

        #endregion
    }
}