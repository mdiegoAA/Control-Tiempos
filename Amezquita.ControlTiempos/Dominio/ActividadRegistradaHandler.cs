using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace Amezquita.ControlTiempos.Dominio
{
    public class ActividadRegistradaHandler : IDomainEventHandler<ActividadRegistrada>
    {
        public void Handle(ActividadRegistrada domainEvent)
        {
            try
            {
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress("Control de Tiempos", "ct@amezquita.com.co"));
                message.To.Add(new MailboxAddress(domainEvent.Proyecto.Gerente.Nombre, domainEvent.Proyecto.Gerente.Email));
                message.Subject = "Aprobar Registro de Tiempos";

                message.Body = new TextPart("plain")
                {
                    Text = string.Format("Se han registrado tiempos para una actividad '{0}' que necesita de su aprobación en el proyecto '{1}'.", domainEvent.Actividad.Nombre, domainEvent.Proyecto.Nombre)
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate("ct@amezquita.com.co", "irjqxoclqzhfkbgr");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}