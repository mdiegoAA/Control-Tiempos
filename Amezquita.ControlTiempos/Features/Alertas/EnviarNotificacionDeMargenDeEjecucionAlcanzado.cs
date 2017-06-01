using MailKit.Net.Smtp;
using MediatR;
using MimeKit;
using System;
using System.Configuration;

namespace Amezquita.ControlTiempos.Features.Alertas
{
    public class EnviarNotificacionDeMargenDeEjecucionAlcanzado
    {
        public class Command : IRequest
        {
            public string Proyecto { get; set; }
            public string Email { get; set; }
            public decimal BolsaHoras { get; set; }
            public decimal PorcentajeEjecutado { get; set; }
            public int? TotalHoras { get; set; }
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
                    msg.To.Add(new MailboxAddress(message.Email, message.Email));
                    msg.Subject = "Margen de Ejecución Alacanzado";

                    msg.Body = new TextPart("plain")
                    {
                        Text = string.Format("El proyecto '{0}' ha alcanzado el margen de ejecución configurado. Porcentaje Configurado: {1}", message.Proyecto, message.PorcentajeEjecutado)
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
