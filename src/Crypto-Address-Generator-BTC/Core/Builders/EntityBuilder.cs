namespace CryptoAddressGeneratorBTC.Core.Builders
{
    public interface IEntityBuilder<TEntity> where TEntity : class, new()
    {
        IEntityBuilder<TEntity> With(Action<TEntity> configure);
        TEntity Build();
        IEnumerable<TEntity> BuildMany(int count);
    }

    public class EntityBuilder<TEntity> : IEntityBuilder<TEntity> where TEntity : class, new()
    {
        private readonly List<Action<TEntity>> _configurations = new();

        public IEntityBuilder<TEntity> With(Action<TEntity> configure)
        {
            _configurations.Add(configure);
            return this;
        }

        public TEntity Build()
        {
            var entity = new TEntity();
            foreach (var configure in _configurations)
                configure(entity);
            return entity;
        }

        public IEnumerable<TEntity> BuildMany(int count)
        {
            for (int i = 0; i < count; i++)
                yield return Build();
        }
    }

    public static class EntityBuilder
    {
        public static EntityBuilder<TEntity> For<TEntity>() where TEntity : class, new() => new();
    }
}
