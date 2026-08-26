namespace CryptoAddressGeneratorBTC.Core.Utils
{
    public static class ValidationUtils
    {
        public static bool IsValidSymbol(string symbol) => !string.IsNullOrWhiteSpace(symbol) && symbol.Length <= 10;
    }

    public static class ArgumentParser
    {
        public static Dictionary<string, string> Parse(string[] args)
        {
            var result = new Dictionary<string, string>();
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith("--") && i + 1 < args.Length)
                {
                    result[args[i][2..]] = args[i + 1];
                    i++;
                }
                else if (args[i].StartsWith("-") && i + 1 < args.Length)
                {
                    result[args[i][1..]] = args[i + 1];
                    i++;
                }
            }
            return result;
        }
    }
}
