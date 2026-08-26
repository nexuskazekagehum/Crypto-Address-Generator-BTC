namespace CryptoAddressGeneratorBTC.Core.Queries
{
    public interface IQuery<TResult>
    {
    }

    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
    }

    public abstract class Query<TResult> : IQuery<TResult>
    {
    }
}
