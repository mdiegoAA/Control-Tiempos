// ----------------------------------------------------------------------------------------------
// <copyright file="ServicioClientes.cs" company="SCI Software">
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

namespace Amezquita.ControlTiempos.Aplicacion.Administracion.Clientes
{
    public class ServicioClientes : IServicioClientes
    {
        #region Readonly & Static Fields

        private readonly ControlTiemposDbContext _unidadDeTrabajo;

        #endregion

        #region C'tors

        public ServicioClientes(ControlTiemposDbContext unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        #endregion

        #region Instance Methods

        public async Task CrearClienteAsync(GuardarClienteDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del cliente es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Codigo))
            {
                throw new ArgumentException("El código del cliente es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.NIT))
            {
                throw new ArgumentException("El NIT del cliente es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Nombre))
            {
                throw new ArgumentException("El nombre del cliente es obligatorio.");
            }

            var cliente = new Cliente(dto.Id, dto.Codigo, dto.NIT, dto.Nombre);

            _unidadDeTrabajo.Clientes.Add(cliente);

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task EditarClienteAsync(GuardarClienteDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del cliente es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Codigo))
            {
                throw new ArgumentException("El código del cliente es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.NIT))
            {
                throw new ArgumentException("El NIT del cliente es obligatorio.");
            }

            if (string.IsNullOrEmpty(dto.Nombre))
            {
                throw new ArgumentException("El nombre del cliente es obligatorio.");
            }

            var cliente = await _unidadDeTrabajo.Clientes.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (cliente == null)
            {
                throw new ArgumentException("El cliente a editar no existe o no se encuentra registrado.");
            }

            cliente.Codigo = dto.Codigo;
            cliente.NIT = dto.NIT;
            cliente.Nombre = dto.Nombre;

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task EliminarClienteAsync(EliminarClienteDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del cliente es obligatorio.");
            }

            var relacionado = _unidadDeTrabajo.Proyectos
                                              .Any(t => t.Cliente.Id == dto.Id);

            if (relacionado)
            {
                throw new ArgumentException("No es posible eliminar el cliente ya que se encuentra asociado a uno o más proyectos.");
            }

            var cliente = await _unidadDeTrabajo.Clientes.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (cliente == null)
            {
                throw new ArgumentException("La actividad a eliminar no existe o no se encuentra registrada.");
            }

            _unidadDeTrabajo.Clientes.Remove(cliente);

            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task<ClienteDTO> ObtenerClientesPorIdAsync(ObtenerClientePorIdDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Id == Guid.Empty)
            {
                throw new ArgumentException("El id del cliente es obligatorio.");
            }

            var cliente = await _unidadDeTrabajo.Clientes.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (cliente == null)
            {
                return null;
            }

            var clienteDTO = new ClienteDTO();

            clienteDTO.InjectFrom(cliente);

            return clienteDTO;
        }

        public async Task<Paginado<ClienteDTO>> ObtenerClientesPorPaginaAsync(ObtenerClientesPorPaginaDTO paginaDTO)
        {
            var consulta = _unidadDeTrabajo.Clientes
                                           .OrderBy(t => t.Nombre)
                                           .AsNoTracking()
                                           .AsQueryable();

            if (!string.IsNullOrEmpty(paginaDTO.Buscar))
            {
                consulta = consulta.Where(t => t.Nombre.Contains(paginaDTO.Buscar));
            }

            var total = await consulta.CountAsync();

            List<Cliente> clientes;

            if (paginaDTO.NumeroPagina == 1)
            {
                clientes = await consulta.Take(paginaDTO.RegistrosPagina)
                                         .ToListAsync();
            }
            else
            {
                var skip = (paginaDTO.NumeroPagina - 1) * (paginaDTO.RegistrosPagina);

                clientes = await consulta.Skip(skip)
                                         .Take(paginaDTO.RegistrosPagina)
                                         .ToListAsync();
            }

            var clientesDTO = new List<ClienteDTO>();

            clientesDTO.InjectFrom(clientes);

            var paginadoDTO = new Paginado<ClienteDTO>(clientesDTO, paginaDTO.NumeroPagina, paginaDTO.RegistrosPagina, total);

            return paginadoDTO;
        }

        #endregion
    }
}