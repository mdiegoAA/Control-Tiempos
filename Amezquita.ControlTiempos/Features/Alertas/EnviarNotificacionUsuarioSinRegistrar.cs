using MailKit.Net.Smtp;
using MediatR;
using MimeKit;
using System;
using System.Configuration;

namespace Amezquita.ControlTiempos.Features.Alertas
{
    public class EnviarNotificacionUsuarioSinRegistrar
    {
        public class Command : IRequest
        {
            public string NombreProyecto { get; set; }
            public string EmailGerente { get; set; }
            public int DiasSinRegistrar { get; set; }
            public string NombreUsuario { get; set; }
            public string EmailUsuario { get; set; }
            public DateTime FechaRegistro { get; set; }
            public int? TotalDias { get; set; }
        }

        public class Handler : RequestHandler<Command>
        {
            public string Email => ConfigurationManager.AppSettings["Notificaciones:Email"];
            public string Nombre => ConfigurationManager.AppSettings["Notificaciones:Nombre"];
            public string SMTP => ConfigurationManager.AppSettings["Notificaciones:SMTP"];
            public int SMTPPuerto => Convert.ToInt32(ConfigurationManager.AppSettings["Notificaciones:SMTPPuerto"]);
            public string Key => ConfigurationManager.AppSettings["Notificaciones:Key"];

            protected override void HandleCore(Command message)
            {
                try
                {
                    var msg = new MimeMessage();

                    msg.From.Add(new MailboxAddress(Nombre, Email));
                    msg.To.Add(new MailboxAddress(message.EmailGerente, message.EmailGerente));
                    msg.Subject = "Usuario Sin Registrar Tiempos";

                    msg.Body = new TextPart("plain")
                    {
                        Text = string.Format("El usuario '{0}' hace {1} días que no registra tiempos en el proyecto {2}.", message.NombreUsuario, message.TotalDias, message.NombreProyecto)
                    };

                    using (var client = new SmtpClient())
                    {
                        client.Connect(SMTP, SMTPPuerto, false);
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        client.Authenticate(Email, Key);
                        client.Send(msg);
                        client.Disconnect(true);
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
