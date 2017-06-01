// ----------------------------------------------------------------------------------------------
// <copyright file="ServicioAreas.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Aplicacion</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Amezquita.ControlTiempos.Dominio;
using Amezquita.ControlTiempos.Infraestructura.Datos;
using Amezquita.ControlTiempos.Infraestructura;
using Amezquita.ControlTiempos.Infraestructura.Extensiones;
using Omu.ValueInjecter;

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Areas
{
    public class ServicioAreas : IServicioAreas
    {
        #region Readonly & Static Fields

        private readonly ControlTiemposDbContext _unidadDeTrabajo;

        #endregion

        #region C'tors

        public ServicioAreas(ControlTiemposDbContext unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        #endregion

        #region Instance Methods

        public async Task CrearAreaAsync(GuardarAreaDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del área es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Codigo))
            {
                throw new ArgumentException("El código del área es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Nombre))
            {
                throw new ArgumentException("El nombre del área es obligatorio.");
            }

            var area = new Area(dto.Id, dto.Codigo, dto.Nombre);

            _unidadDeTrabajo.Areas.Add(area);

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task EditarAreaAsync(GuardarAreaDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del ára es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Codigo))
            {
                throw new ArgumentException("El código del área es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Nombre))
            {
                throw new ArgumentException("El nombre del área es obligatorio.");
            }

            var area = await _unidadDeTrabajo.Areas.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (area == null)
            {
                throw new ArgumentException("El área a editar no existe o no se encuentra registrada.");
            }

            area.Codigo = dto.Codigo;
            area.Nombre = dto.Nombre;

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task EliminarAreaAsync(EliminarAreaDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del área es obligatorio.");
            }

            var relacionada = _unidadDeTrabajo.Proyectos
                                              .Any(t => t.AreasProyecto.Any(a => a.Id == dto.Id));

            if (relacionada)
            {
                throw new ArgumentException("No es posible eliminar el área ya que se encuentra asociada a uno o más proyectos.");
            }

            var area = await _unidadDeTrabajo.Areas.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (area == null)
            {
                throw new ArgumentException("El área a eliminar no existe o no se encuentra registrada.");
            }

            _unidadDeTrabajo.Areas.Remove(area);

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task<AreaDTO> ObtenerAreaPorIdAsync(ObtenerAreaPorIdDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del área es obligatorio.");
            }

            var area = await _unidadDeTrabajo.Areas.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (area == null)
            {
                return null;
            }

            var areaDTO = new AreaDTO();

            areaDTO.InjectFrom(area);

            return areaDTO;
        }

        public async Task<Paginado<AreaDTO>> ObtenerAreasPorPaginaAsync(ObtenerAreasPorPaginaDTO paginaDTO)
        {
            var consulta = _unidadDeTrabajo.Areas
                                           .OrderBy(t => t.Nombre)
                                           .AsNoTracking()
                                           .AsQueryable();

            if (!string.IsNullOrEmpty(paginaDTO.Buscar))
            {
                consulta = consulta.Where(t => t.Nombre.Contains(paginaDTO.Buscar));
            }

            var total = await consulta.CountAsync();

            List<Area> areas;

            if (paginaDTO.NumeroPagina == 1)
            {
                areas = await consulta.Take(paginaDTO.RegistrosPagina)
                                      .ToListAsync();
            }
            else
            {
                var skip = (paginaDTO.NumeroPagina - 1) * (paginaDTO.RegistrosPagina);

                areas = await consulta.Skip(skip)
                                      .Take(paginaDTO.RegistrosPagina)
                                      .ToListAsync();
            }

            var areasDTO = new List<AreaDTO>();

            areasDTO.InjectFrom(areas);

            var paginadoDTO = new Paginado<AreaDTO>(areasDTO, paginaDTO.NumeroPagina, paginaDTO.RegistrosPagina, total);

            return paginadoDTO;
        }

        #endregion
    }
}