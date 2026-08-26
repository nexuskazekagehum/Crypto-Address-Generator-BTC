namespace CryptoAddressGeneratorBTC.Core.Analyzers
{
    public interface ISummaryAnalyzer<T>
    {
        SummaryResult<T> Analyze(IEnumerable<T> items);
    }

    public class SummaryResult<T>
    {
        public int Count { get; set; }
        public T? First { get; set; }
        public T? Last { get; set; }
        public Dictionary<string, object> Aggregates { get; set; } = new();
        public List<string> Warnings { get; set; } = new();
    }

    public class GenericSummaryAnalyzer<T> : ISummaryAnalyzer<T>
    {
        public SummaryResult<T> Analyze(IEnumerable<T> items)
        {
            var list = items.ToList();
            return new SummaryResult<T>
            {
                Count = list.Count,
                First = list.FirstOrDefault(),
                Last = list.LastOrDefault(),
                Aggregates = { ["timestamp"] = DateTime.UtcNow }
            };
        }
    }
}
