// ----------------------------------------------------------------------------------------------
// <copyright file="ServicioCalendarios.cs" company="SCI Software">
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

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Calendarios
{
    public class ServicioCalendarios : IServicioCalendarios
    {
        #region Readonly & Static Fields

        private readonly ControlTiemposDbContext _unidadDeTrabajo;

        #endregion

        #region C'tors

        public ServicioCalendarios(ControlTiemposDbContext unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        #endregion

        #region Instance Methods

        public async Task CrearDiaCalendarioAsync(GuardarDiaCalendarioDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del día calendario es obligatorio.");
            }

            if (dto.Dia == DateTime.MinValue)
            {
                throw new ArgumentException("El día calendario es obligatorio.");
            }

            var existe = await _unidadDeTrabajo.DiasCalendario.AnyAsync(c => c.Dia == DbFunctions.TruncateTime(dto.Dia));

            if (existe)
            {
                throw new InvalidOperationException("El día calendario ya se encuentra registrado.");
            }

            var diaCalendario = new DiaCalendario(dto.Id, dto.Dia, dto.EsFestivo);

            _unidadDeTrabajo.DiasCalendario.Add(diaCalendario);

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task EditarDiaCalendarioAsync(GuardarDiaCalendarioDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id día calendario es obligatorio.");
            }

            if (dto.Dia == DateTime.MinValue)
            {
                throw new ArgumentException("El día calendario es obligatorio.");
            }

            var diaCalendario = await _unidadDeTrabajo.DiasCalendario.FindAsync(dto.Id);

            if (diaCalendario == null)
            {
                throw new ArgumentException("El día calendario a editar no existe o no se encuentra registrada.");
            }

            diaCalendario.Dia = dto.Dia;
            diaCalendario.EsFestivo = dto.EsFestivo;

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task EliminarDiaCalendarioAsync(EliminarDiaCalendarioDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del  día calendario es obligatorio.");
            }

            var diaCalendario = await _unidadDeTrabajo.DiasCalendario.FindAsync(dto.Id);

            if (diaCalendario == null)
            {
                throw new ArgumentException("El día calendario a eliminar no existe o no se encuentra registrada.");
            }

            _unidadDeTrabajo.DiasCalendario.Remove(diaCalendario);

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task<DiaCalendarioDTO> ObtenerDiaCalendarioAsync(ObtenerDiaCalendarioDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El día calendario es obligatorio.");
            }

            var diaCalendario = await _unidadDeTrabajo.DiasCalendario.FindAsync(dto.Id);

            if (diaCalendario == null)
            {
                return null;
            }

            var diaCalendarioDTO = new DiaCalendarioDTO();

            diaCalendarioDTO.InjectFrom(diaCalendario);

            return diaCalendarioDTO;
        }

        public async Task<Paginado<DiaCalendarioDTO>> ObtenerDiasCalendarioPorPaginasAsync(ObtenerDiasCalendarioPorPaginaDTO paginaDTO)
        {
            var consulta = _unidadDeTrabajo.DiasCalendario
                                           .OrderByDescending(t => t.Dia)
                                           .AsNoTracking()
                                           .AsQueryable();

            var total = await consulta.CountAsync();

            List<DiaCalendario> diasCalendario;

            if (paginaDTO.NumeroPagina == 1)
            {
                diasCalendario = await consulta.Take(paginaDTO.RegistrosPagina)
                                               .ToListAsync();
            }
            else
            {
                var skip = (paginaDTO.NumeroPagina - 1) * (paginaDTO.RegistrosPagina);

                diasCalendario = await consulta.Skip(skip)
                                               .Take(paginaDTO.RegistrosPagina)
                                               .ToListAsync();
            }

            var diasCalendarioDTO = new List<DiaCalendarioDTO>();

            diasCalendarioDTO.InjectFrom(diasCalendario);

            var paginadoDTO = new Paginado<DiaCalendarioDTO>(diasCalendarioDTO, paginaDTO.NumeroPagina, paginaDTO.RegistrosPagina, total);

            return paginadoDTO;
        }

        #endregion
    }
}