// ----------------------------------------------------------------------------------------------
// <copyright file="IServicioClientes.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Amezquita.ControlTiempos.Infraestructura;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Clientes
{
    public interface IServicioClientes
    {
        #region Instance Methods

        Task CrearClienteAsync(GuardarClienteDTO dto);

        Task EditarClienteAsync(GuardarClienteDTO dto);

        Task EliminarClienteAsync(EliminarClienteDTO dto);

        Task<ClienteDTO> ObtenerClientesPorIdAsync(ObtenerClientePorIdDTO dto);

        Task<Paginado<ClienteDTO>> ObtenerClientesPorPaginaAsync(ObtenerClientesPorPaginaDTO paginaDTO);

        #endregion
    }
}