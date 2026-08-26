namespace CryptoAddressGeneratorBTC.Core.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public T? Value { get; }
        public string Error { get; }
        public IReadOnlyList<string> Errors { get; }

        private Result(bool isSuccess, T? value, string error, List<string>? errors)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
            Errors = errors?.AsReadOnly() ?? new List<string>().AsReadOnly();
        }

        public static Result<T> Success(T value) => new(true, value, string.Empty, null);
        public static Result<T> Failure(string error) => new(false, default, error, new List<string> { error });
        public static Result<T> Failure(List<string> errors) => new(false, default, errors.FirstOrDefault() ?? "Unknown error", errors);

        public Result<TNew> Map<TNew>(Func<T, TNew> mapper)
        {
            return IsSuccess ? Result<TNew>.Success(mapper(Value!)) : Result<TNew>.Failure(Error);
        }

        public async Task<Result<TNew>> MapAsync<TNew>(Func<T, Task<TNew>> mapper)
        {
            if (!IsSuccess) return Result<TNew>.Failure(Error);
            return Result<TNew>.Success(await mapper(Value!));
        }

        public T Unwrap() => IsSuccess ? Value! : throw new InvalidOperationException(Error);
    }

    public class Result
    {
        public static Result<T> Success<T>(T value) => Result<T>.Success(value);
        public static Result<T> Failure<T>(string error) => Result<T>.Failure(error);
    }
}
