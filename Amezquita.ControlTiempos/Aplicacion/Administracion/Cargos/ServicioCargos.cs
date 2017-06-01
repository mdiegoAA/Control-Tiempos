// ----------------------------------------------------------------------------------------------
// <copyright file="ServicioCargos.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
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

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Cargos
{
    public class ServicioCargos : IServicioCargos
    {
        #region Readonly & Static Fields

        private readonly ControlTiemposDbContext _unidadDeTrabajo;

        #endregion

        #region C'tors

        public ServicioCargos(ControlTiemposDbContext unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        #endregion

        #region Instance Methods

        public async Task CrearCargoAsync(GuardarCargoDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del cargo es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Codigo))
            {
                throw new ArgumentException("El código del cargo es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Nombre))
            {
                throw new ArgumentException("El nombre del cargo es obligatorio.");
            }

            if (dto.Tarifa <= 0.0m)
            {
                throw new ArgumentException("El valor de tarifa del cargo debe ser mayor que cero.");
            }

            var cargo = new Cargo(dto.Id, dto.Codigo, dto.Nombre, dto.Tarifa);

            _unidadDeTrabajo.Cargos.Add(cargo);

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task EditarCargoAsync(GuardarCargoDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del cargo es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Codigo))
            {
                throw new ArgumentException("El código del cargo es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Nombre))
            {
                throw new ArgumentException("El nombre del cargo es obligatorio.");
            }

            if (dto.Tarifa <= 0.0m)
            {
                throw new ArgumentException("El valor de tarifa del cargo debe ser mayor que cero.");
            }

            var cargo = await _unidadDeTrabajo.Cargos.FirstOrDefaultAsync(c => c.Id == dto.Id);

            if (cargo == null)
            {
                throw new ArgumentException("El cargo a editar no existe o no se encuentra registrado.");
            }

            cargo.Codigo = dto.Codigo;
            cargo.Nombre = dto.Nombre;
            cargo.Tarifa = dto.Tarifa;

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task EliminarCargoAsync(EliminarCargoDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del cargo es obligatorio.");
            }

            var cargo = await _unidadDeTrabajo.Cargos.FirstOrDefaultAsync(c => c.Id == dto.Id);

            if (cargo == null)
            {
                throw new ArgumentException("El cargo a eliminar no existe o no se encuentra registrado.");
            }

            _unidadDeTrabajo.Cargos.Remove(cargo);

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task<CargoDTO> ObtenerCargosPorIdAsync(ObtenerCargoPorIdDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del cargo es obligatorio.");
            }

            var cargo = await _unidadDeTrabajo.Cargos.FirstOrDefaultAsync(c => c.Id == dto.Id);

            if (cargo == null)
            {
                return null;
            }

            var cargoDTO = new CargoDTO();

            cargoDTO.InjectFrom(cargo);

            return cargoDTO;
        }

        public async Task<Paginado<CargoDTO>> ObtenerCargosPorPaginaAsync(ObtenerCargosPorPaginaDTO paginaDTO)
        {
            var consulta = _unidadDeTrabajo.Cargos
                                           .OrderBy(t => t.Nombre)
                                           .AsNoTracking()
                                           .AsQueryable();

            if (!string.IsNullOrEmpty(paginaDTO.Buscar))
            {
                consulta = consulta.Where(t => t.Nombre.Contains(paginaDTO.Buscar));
            }

            var total = await consulta.CountAsync();

            List<Cargo> actividades;

            if (paginaDTO.NumeroPagina == 1)
            {
                actividades = await consulta.Take(paginaDTO.RegistrosPagina)
                                            .ToListAsync();
            }
            else
            {
                var skip = (paginaDTO.NumeroPagina - 1) * (paginaDTO.RegistrosPagina);

                actividades = await consulta.Skip(skip)
                                            .Take(paginaDTO.RegistrosPagina)
                                            .ToListAsync();
            }

            var actividadesDTO = new List<CargoDTO>();

            actividadesDTO.InjectFrom(actividades);

            var paginadoDTO = new Paginado<CargoDTO>(actividadesDTO, paginaDTO.NumeroPagina, paginaDTO.RegistrosPagina, total);

            return paginadoDTO;
        }

        #endregion
    }
}