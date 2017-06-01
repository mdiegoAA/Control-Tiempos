// ----------------------------------------------------------------------------------------------
// <copyright file="ServicioServicios.cs" company="SCI Software">
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

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Servicios
{
    public class ServicioServicios : IServicioServicios
    {
        #region Readonly & Static Fields

        private readonly ControlTiemposDbContext _unidadDeTrabajo;

        #endregion

        #region C'tors

        public ServicioServicios(ControlTiemposDbContext unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        #endregion

        #region Instance Methods

        public async Task CrearServicioAsync(GuardarServicioDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del servicio es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Codigo))
            {
                throw new ArgumentException("El código del servicio es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Nombre))
            {
                throw new ArgumentException("El nombre del servicio es obligatorio.");
            }

            var servicio = new Servicio(dto.Id, dto.Codigo, dto.Nombre, dto.EsGenerico);

            _unidadDeTrabajo.Servicios.Add(servicio);

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task EditarServicioAsync(GuardarServicioDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del servicio es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Codigo))
            {
                throw new ArgumentException("El código del servicio es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Nombre))
            {
                throw new ArgumentException("El nombre del servicio es obligatorio.");
            }

            var servicio = await _unidadDeTrabajo.Servicios.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (servicio == null)
            {
                throw new ArgumentException("El servicio a editar no existe o no se encuentra registrado.");
            }

            servicio.Codigo = dto.Codigo;
            servicio.Nombre = dto.Nombre;
            servicio.EsGenerico = dto.EsGenerico;

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task EliminarServicioAsync(EliminarServicioDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del servicio es obligatorio.");
            }

            var relacionado = _unidadDeTrabajo.Proyectos
                                              .Any(t => t.Servicios.Any(s => s.Id == dto.Id));

            if (relacionado)
            {
                throw new ArgumentException("No es posible eliminar el servicio ya que se encuentra asociado a uno o más proyectos.");
            }

            var servicio = await _unidadDeTrabajo.Servicios.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (servicio == null)
            {
                throw new ArgumentException("El servicio a eliminar no existe o no se encuentra registrado.");
            }

            _unidadDeTrabajo.Servicios.Remove(servicio);

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task<ServicioDTO> ObtenerServicioPorIdAsync(ObtenerServicioPorIdDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del servicio es obligatorio.");
            }

            var servicio = await _unidadDeTrabajo.Servicios.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (servicio == null)
            {
                return null;
            }

            var servicioDTO = new ServicioDTO();

            servicioDTO.InjectFrom(servicio);

            return servicioDTO;
        }

        public async Task<Paginado<ServicioDTO>> ObtenerServiciosPorPaginaAsync(ObtenerServiciosPorPaginaDTO paginaDTO)
        {
            var consulta = _unidadDeTrabajo.Servicios
                                           .OrderBy(t => t.Nombre)
                                           .AsNoTracking()
                                           .AsQueryable();

            if (!string.IsNullOrEmpty(paginaDTO.Buscar))
            {
                consulta = consulta.Where(t => t.Nombre.Contains(paginaDTO.Buscar));
            }

            var total = await consulta.CountAsync();

            List<Servicio> servicios;

            if (paginaDTO.NumeroPagina == 1)
            {
                servicios = await consulta.Take(paginaDTO.RegistrosPagina)
                                          .ToListAsync();
            }
            else
            {
                var skip = (paginaDTO.NumeroPagina - 1) * paginaDTO.RegistrosPagina;

                servicios = await consulta.Skip(skip)
                                          .Take(paginaDTO.RegistrosPagina)
                                          .ToListAsync();
            }

            var serviciosDTO = new List<ServicioDTO>();

            serviciosDTO.InjectFrom(servicios);

            var paginadoDTO = new Paginado<ServicioDTO>(serviciosDTO, paginaDTO.NumeroPagina, paginaDTO.RegistrosPagina, total);

            return paginadoDTO;
        }

        #endregion
    }
}