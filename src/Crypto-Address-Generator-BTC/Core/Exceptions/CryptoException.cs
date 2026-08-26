namespace CryptoAddressGeneratorBTC.Core.Exceptions
{
    public class CryptoException : Exception
    {
        public CryptoException(string message) : base(message) { }
        public CryptoException(string message, Exception inner) : base(message, inner) { }
    }
}
