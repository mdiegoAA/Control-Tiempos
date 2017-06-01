// ----------------------------------------------------------------------------------------------
// <copyright file="IServicioUsuarios.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Amezquita.ControlTiempos.Infraestructura;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Usuarios
{
    public interface IServicioUsuarios
    {
        #region Instance Methods

        Task CrearUsuarioAsync(GuardarUsuarioDTO dto);

        Task EditarUsuarioAsync(GuardarUsuarioDTO dto);

        Task EliminarUsuarioAsync(EliminarUsuarioDTO dto);

        Task GuardarRolesUsuarioAsync(GuardarRolesUsuarioDTO dto);

        Task<List<CargoDTO>> ObtenerCargosAsync();

        Task<List<RolDTO>> ObtenerRolesAsync();

        Task<List<RolDTO>> ObtenerRolesPorUsuarioAsync(ObteneRolesPorUsuarioDTO dto);

        Task<UsuarioDTO> ObtenerUsuarioPorIdAsync(ObtenerUsuarioPorIdDTO dto);

        Task<Paginado<UsuarioDTO>> ObtenerUsuariosPorPaginaAsync(ObtenerUsuariosPorPaginaDTO paginaDTO);

        #endregion
    }
}