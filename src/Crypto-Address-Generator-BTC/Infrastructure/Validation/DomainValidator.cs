namespace CryptoAddressGeneratorBTC.Infrastructure.Validation
{
    public interface IRequestValidator<T>
    {
        Task<ValidationResult> ValidateAsync(T request, CancellationToken cancellationToken = default);
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; } = true;
        public List<string> Errors { get; set; } = new();
    }

    public class DefaultRequestValidator<T> : IRequestValidator<T> where T : class
    {
        public Task<ValidationResult> ValidateAsync(T request, CancellationToken cancellationToken = default)
        {
            if (request is null)
                return Task.FromResult(new ValidationResult { IsValid = false, Errors = { "Request is null" } });
            return Task.FromResult(new ValidationResult { IsValid = true });
        }
    }
}
