using System.Collections.Generic;

namespace Amezquita.ControlTiempos.Dominio
{
    public interface IEntity
    {
        ICollection<IDomainEvent> Events { get; }
    }

    public abstract class Entity<TId> : IEntity
    {
        public ICollection<IDomainEvent> Events { get; private set; } = new HashSet<IDomainEvent>();

        public TId Id { get; protected set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity<TId>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            var entity = (Entity<TId>)obj;

            return entity.Id.Equals(Id);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Entity<TId> left, Entity<TId> right) => Equals(left, null) ? (Equals(right, null)) : left.Equals(right);

        public static bool operator !=(Entity<TId> left, Entity<TId> right) => !(left == right);
    }
}