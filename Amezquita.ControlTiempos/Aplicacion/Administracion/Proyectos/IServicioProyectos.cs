// ----------------------------------------------------------------------------------------------
// <copyright file="IServicioProyectos.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Amezquita.ControlTiempos.Infraestructura;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Proyectos
{
    public interface IServicioProyectos
    {
        #region Instance Methods

        Task CrearProyectoAsync(GuardarProyectoDTO dto);

        Task EditarProyectoAsync(GuardarProyectoDTO dto);

        Task EliminarProyectoAsync(EliminarProyectoDTO dto);

        Task GuardarActividadesProyectoAsync(GuardarActividadesProyectoDTO dto);

        Task GuardarAreasProyectoAsync(GuardarAreasProyectoDTO dto);

        Task GuardarGrupoDeTrabajoProyectoAsync(GuardarGrupoDeTrabajoProyectoDTO dto);

        Task GuardarServiciosProyectoAsync(GuardarServiciosProyectoDTO dto);

        Task<List<ActividadDTO>> ObtenerActvidadesAsync();

        Task<List<ActividadDTO>> ObtenerActvidadesPorProyectoAsync(ObtenerActividadesPorProyectoDTO dto);

        Task<List<AreaDTO>> ObtenerAreasAsync();

        Task<List<AreaProyectoDTO>> ObtenerAreasProyectoAsync(ObtenerAreasProyectoDTO dto);

        Task<List<ClienteDTO>> ObtenerClientesAsync();

        Task<List<UsuarioDTO>> ObtenerGrupoDeTrabajoPorProyectoAsync(ObtenerGrupoDeTrabajoPorProyectoDTO dto);

        Task<ProyectoDTO> ObtenerProyectoPorIdAsync(ObtenerProyectoPorIdDTO dto);

        Task<Paginado<ProyectoDTO>> ObtenerProyectosPorPaginaAsync(ObtenerProyectosPorPaginaDTO paginaDTO);

        Task<List<ServicioDTO>> ObtenerServiciosAsync();

        Task<List<ServicioDTO>> ObtenerServiciosPorProyectoAsync(ObtenerServiciosPorProyectoDTO dto);

        Task<List<UsuarioDTO>> ObtenerUsuariosAsync();

        #endregion
    }
}