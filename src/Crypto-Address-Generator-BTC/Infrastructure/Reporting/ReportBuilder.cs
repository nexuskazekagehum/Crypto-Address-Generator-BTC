using CryptoAddressGeneratorBTC.Core.Models;
using System.Text.Json;

namespace CryptoAddressGeneratorBTC.Infrastructure.Reporting
{
    public interface IReportBuilder
    {
        Task<Report> BuildJsonReportAsync<T>(string title, T data, CancellationToken cancellationToken = default);
        Task<Report> BuildCsvReportAsync<T>(string title, IEnumerable<T> rows, Func<T, string[]> mapper, CancellationToken cancellationToken = default);
    }

    public class DefaultReportBuilder : IReportBuilder
    {
        public Task<Report> BuildJsonReportAsync<T>(string title, T data, CancellationToken cancellationToken = default)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return Task.FromResult(new Report { Title = title, Format = "json", Content = json });
        }

        public Task<Report> BuildCsvReportAsync<T>(string title, IEnumerable<T> rows, Func<T, string[]> mapper, CancellationToken cancellationToken = default)
        {
            var lines = rows.Select(mapper).Select(cols => string.Join(",", cols));
            return Task.FromResult(new Report { Title = title, Format = "csv", Content = string.Join("
", lines) });
        }
    }
}
