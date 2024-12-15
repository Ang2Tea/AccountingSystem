namespace AccountingSystem.Domain.Core
{
    public abstract class Entity<T>
    {
        public T Id { get; private set; }

        protected Entity() {}
        protected Entity(T id)
        {
            Id = id;
        }
    }
}