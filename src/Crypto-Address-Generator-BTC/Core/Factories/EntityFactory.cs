namespace CryptoAddressGeneratorBTC.Core.Factories
{
    public interface IEntityFactory<TEntity> where TEntity : class
    {
        TEntity Create();
        TEntity Create(string identifier);
    }

    public abstract class EntityFactory<TEntity> : IEntityFactory<TEntity> where TEntity : class
    {
        public abstract TEntity Create();

        public virtual TEntity Create(string identifier)
        {
            var entity = Create();
            ApplyIdentifier(entity, identifier);
            return entity;
        }

        protected abstract void ApplyIdentifier(TEntity entity, string identifier);
    }

    public class DefaultEntityFactory<TEntity> : EntityFactory<TEntity> where TEntity : class, new()
    {
        public override TEntity Create() => new();

        protected override void ApplyIdentifier(TEntity entity, string identifier)
        {
            var idProperty = typeof(TEntity).GetProperty("Id") ?? typeof(TEntity).GetProperty("Identifier");
            idProperty?.SetValue(entity, identifier);
        }
    }
}
