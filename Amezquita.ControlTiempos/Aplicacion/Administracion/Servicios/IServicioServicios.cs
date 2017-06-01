using System.Threading.Tasks;
using Amezquita.ControlTiempos.Infraestructura;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Servicios
{
    public interface IServicioServicios
    {
        #region Instance Methods

        Task CrearServicioAsync(GuardarServicioDTO dto);

        Task EditarServicioAsync(GuardarServicioDTO dto);

        Task EliminarServicioAsync(EliminarServicioDTO dto);

        Task<ServicioDTO> ObtenerServicioPorIdAsync(ObtenerServicioPorIdDTO dto);

        Task<Paginado<ServicioDTO>> ObtenerServiciosPorPaginaAsync(ObtenerServiciosPorPaginaDTO paginaDTO);

        #endregion
    }
}