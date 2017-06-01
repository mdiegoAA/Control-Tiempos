using Amezquita.ControlTiempos.Features.Alertas;
using MediatR;
using Omu.ValueInjecter;

namespace Amezquita.ControlTiempos.WebJobs
{
    public class UsuariosSinRegistrarHaceDiasJob
    {
        IMediator _mediator;

        public UsuariosSinRegistrarHaceDiasJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Procesar()
        {
            var usuarios = _mediator.Send(new ObtenerUsuariosSinRegistrarHaceDias.Query());

            foreach (var usuario in usuarios)
            {
                var cmd = new EnviarNotificacionUsuarioSinRegistrar.Command();
                cmd.InjectFrom(usuario);
                _mediator.Send(cmd);
            }
        }
    }
}