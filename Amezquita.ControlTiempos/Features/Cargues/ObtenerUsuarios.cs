using Amezquita.ControlTiempos.Infraestructura.Datos;
using Amezquita.ControlTiempos.Infraestructura.Extensiones;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Amezquita.ControlTiempos.Features.Cargues
{
    public class ObtenerUsuarios
    {
        public class UsuarioDTO
        {
            public Guid Id { get; set; }
            public string Nombre { get; set; }
        }

        public class Query : IAsyncRequest<IEnumerable<UsuarioDTO>> { }

        public class Handler : IAsyncRequestHandler<Query, IEnumerable<UsuarioDTO>>
        {
            private readonly ControlTiemposDbContext _unidadDeTrabajo;

            public Handler(ControlTiemposDbContext unidadDeTrabajo)
            {
                _unidadDeTrabajo = unidadDeTrabajo;
            }

            public async Task<IEnumerable<UsuarioDTO>> Handle(Query message)
            {
                var usuarios = await _unidadDeTrabajo.Usuarios
                                                     .Select(u => new { u.Id, u.Nombre })
                                                     .OrderBy(u => u.Nombre)
                                                     .ToListAsync();

                return new List<UsuarioDTO>().InjectFrom(usuarios);
            }
        }
    }
}