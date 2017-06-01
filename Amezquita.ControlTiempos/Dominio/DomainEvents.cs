using System;
using System.Collections.Generic;

namespace Amezquita.ControlTiempos.Dominio
{
    public interface IDomainEvent { }

    public interface IDomainEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        void Handle(TDomainEvent domainEvent);
    }

    public static class DomainEvents
    {
        public static Func<Type, IEnumerable<dynamic>> Factory;

        public static void Raise<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        {
            if (Factory != null)
                foreach (var domainEventHandler in Factory(typeof(IDomainEventHandler<TDomainEvent>)))
                    domainEventHandler.Handle(domainEvent);
        }
    }
}