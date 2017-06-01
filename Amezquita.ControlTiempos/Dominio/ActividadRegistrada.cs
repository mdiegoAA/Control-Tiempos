using System;

namespace Amezquita.ControlTiempos.Dominio
{
    public class ActividadRegistrada : IDomainEvent
    {
        public Actividad Actividad { get; private set; }

        public Proyecto Proyecto { get; private set; }

        private ActividadRegistrada() {}

        public ActividadRegistrada(Proyecto proyecto, Actividad actividad)
        {
            if(proyecto == null)
            {
                throw new ArgumentNullException(nameof(proyecto), "El proyecto del evento actividad registrada debe ser diferente de nulo.");
            }

            if(actividad == null)
            {
                throw new ArgumentNullException(nameof(actividad), "La actividad del evento actividad registrada debe ser diferente de nulo.");
            }

            Proyecto = proyecto;
            Actividad = actividad;
        }
    }
}