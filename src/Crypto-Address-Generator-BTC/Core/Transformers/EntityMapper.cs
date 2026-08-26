namespace CryptoAddressGeneratorBTC.Core.Transformers
{
    public interface IEntityMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);
        IEnumerable<TDestination> MapMany(IEnumerable<TSource> sources);
    }

    public abstract class EntityMapper<TSource, TDestination> : IEntityMapper<TSource, TDestination>
    {
        public abstract TDestination Map(TSource source);

        public IEnumerable<TDestination> MapMany(IEnumerable<TSource> sources)
        {
            return sources.Select(Map);
        }

        protected static TValue? GetValueOrDefault<TValue>(TValue? value) => value;
    }

    public class DefaultMapper<TSource, TDestination> : EntityMapper<TSource, TDestination> where TDestination : new()
    {
        public override TDestination Map(TSource source)
        {
            var destination = new TDestination();
            foreach (var sourceProperty in typeof(TSource).GetProperties())
            {
                var destProperty = typeof(TDestination).GetProperty(sourceProperty.Name);
                if (destProperty != null && destProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                    destProperty.SetValue(destination, sourceProperty.GetValue(source));
            }
            return destination;
        }
    }
}
