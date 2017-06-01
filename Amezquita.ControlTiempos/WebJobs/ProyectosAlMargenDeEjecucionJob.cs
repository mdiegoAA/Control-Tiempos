using Amezquita.ControlTiempos.Features.Alertas;
using MediatR;
using Omu.ValueInjecter;

namespace Amezquita.ControlTiempos.WebJobs
{
    public class ProyectosAlMargenDeEjecucionJob
    {
        IMediator _mediator;

        public ProyectosAlMargenDeEjecucionJob(IMediator meditor)
        {
            _mediator = meditor;
        }

        public void Procesar()
        {
            var proyectos = _mediator.Send(new ObtenerProyectoAlMargenDeEjecucion.Query());

            foreach (var proyecto in proyectos)
            {
                var cmd = new EnviarNotificacionDeMargenDeEjecucionAlcanzado.Command();
                cmd.InjectFrom(proyecto);
                _mediator.Send(cmd);
            }
        }
    }
}