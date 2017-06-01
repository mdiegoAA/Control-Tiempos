using Amezquita.ControlTiempos.Infraestructura.Datos;
using Amezquita.ControlTiempos.Infraestructura.Extensiones;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Amezquita.ControlTiempos.Features.Alertas
{
    public class ObtenerUsuariosSinRegistrarHaceDias
    {
        public class UsuarioSinRegistrarHaceDiasDTO
        {
            public string NombreProyecto { get; set; }
            public string EmailGerente { get; set; }
            public int DiasSinRegistrar { get; set; }
            public string NombreUsuario { get; set; }
            public string EmailUsuario { get; set; }
            public DateTime FechaRegistro { get; set; }
            public int? TotalDias { get; set; }
        }

        public class Query : IRequest<IEnumerable<UsuarioSinRegistrarHaceDiasDTO>> { }

        public class Handler : IRequestHandler<Query, IEnumerable<UsuarioSinRegistrarHaceDiasDTO>>
        {
            private readonly ControlTiemposDbContext _unidadDeTrabajo;

            public Handler(ControlTiemposDbContext unidadDeTrabajo)
            {
                _unidadDeTrabajo = unidadDeTrabajo;
            }

            public IEnumerable<UsuarioSinRegistrarHaceDiasDTO> Handle(Query message)
            {
                var usuarios = _unidadDeTrabajo.CarguesHoras
                                               .AsNoTracking()
                                               .Select(ch => new
                                               {
                                                   ProyectoId = ch.Proyecto.Id,
                                                   NombreProyecto = ch.Proyecto.Nombre,
                                                   EmailGerente = ch.Proyecto.Gerente.Email,
                                                   DiasSinRegistrar = ch.Proyecto.AlertasProyecto.DiasSinRegistrar,
                                                   UsuarioId = ch.Usuario.Id,
                                                   NombreUsuario = ch.Usuario.Nombre,
                                                   EmailUsuario = ch.Usuario.Email,
                                                   FechaRegistro = ch.FechaRegistro
                                               })
                                               .GroupBy(ch => new
                                               {
                                                   ProyectoId = ch.ProyectoId,
                                                   NombreProyecto = ch.NombreProyecto,
                                                   EmailGerente = ch.EmailGerente,
                                                   DiasSinRegistrar = ch.DiasSinRegistrar,
                                                   UsuarioId = ch.UsuarioId,
                                                   NombreUsuario = ch.NombreUsuario,
                                                   EmailUsuario = ch.EmailUsuario,
                                               })
                                               .Select(g => new
                                               {
                                                   NombreProyecto = g.Key.NombreProyecto,
                                                   EmailGerente = g.Key.EmailGerente,
                                                   DiasSinRegistrar = g.Key.DiasSinRegistrar,
                                                   NombreUsuario = g.Key.NombreUsuario,
                                                   EmailUsuario = g.Key.EmailUsuario,
                                                   FechaRegistro = g.Max(ch => ch.FechaRegistro),
                                                   TotalDias = DbFunctions.DiffDays(g.Max(ch => ch.FechaRegistro), DateTime.Now),
                                               })
                                               .Where(ch => ch.TotalDias >= ch.DiasSinRegistrar)
                                               .ToList();

                return new List<UsuarioSinRegistrarHaceDiasDTO>().InjectFrom(usuarios);
            }
        }
    }
}