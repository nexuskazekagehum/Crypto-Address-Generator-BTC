using System.Text;

namespace CryptoAddressGeneratorBTC.Infrastructure.Serialization
{
    public interface ICsvSerializer
    {
        string Serialize<T>(IEnumerable<T> items, string[]? headers = null);
        IEnumerable<T> Deserialize<T>(string csv) where T : new();
    }

    public class DefaultCsvSerializer : ICsvSerializer
    {
        public string Serialize<T>(IEnumerable<T> items, string[]? headers = null)
        {
            var sb = new StringBuilder();
            var props = typeof(T).GetProperties();
            var headerNames = headers ?? props.Select(p => p.Name).ToArray();
            sb.AppendLine(string.Join(",", headerNames));
            foreach (var item in items)
            {
                var values = props.Select(p => FormatValue(p.GetValue(item)));
                sb.AppendLine(string.Join(",", values));
            }
            return sb.ToString();
        }

        public IEnumerable<T> Deserialize<T>(string csv) where T : new()
        {
            var lines = csv.Split(new[] { '
', '
' }, StringSplitOptions.RemoveEmptyEntries).Skip(1);
            var props = typeof(T).GetProperties();
            foreach (var line in lines)
            {
                var values = line.Split(',');
                var item = new T();
                for (int i = 0; i < Math.Min(values.Length, props.Length); i++)
                    TrySetValue(props[i], item, values[i]);
                yield return item;
            }
        }

        private string FormatValue(object? value) => value?.ToString()?.Replace(",", ";") ?? "";

        private void TrySetValue(System.Reflection.PropertyInfo prop, object target, string value)
        {
            try
            {
                if (prop.PropertyType == typeof(string))
                    prop.SetValue(target, value);
                else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
                    prop.SetValue(target, int.TryParse(value, out var iv) ? iv : 0);
                else if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(decimal?))
                    prop.SetValue(target, decimal.TryParse(value, out var dv) ? dv : 0m);
                else if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
                    prop.SetValue(target, bool.TryParse(value, out var bv) && bv);
            }
            catch { }
        }
    }
}
