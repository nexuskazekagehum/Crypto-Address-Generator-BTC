namespace CryptoAddressGeneratorBTC.Core.Models
{
    public class PagedList<T>
    {
        public IReadOnlyList<T> Items { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public PagedList(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
        {
            Items = items.ToList().AsReadOnly();
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public PagedList<TNew> Map<TNew>(Func<T, TNew> mapper)
        {
            return new PagedList<TNew>(Items.Select(mapper), PageNumber, PageSize, TotalCount);
        }
    }
}
