namespace CryptoAddressGeneratorBTC.Infrastructure.Security
{
    public interface IPermissionChecker
    {
        bool HasPermission(string actor, string resource, string action);
        Task<bool> HasPermissionAsync(string actor, string resource, string action, CancellationToken cancellationToken = default);
    }

    public class DefaultPermissionChecker : IPermissionChecker
    {
        private readonly Dictionary<string, List<string>> _roles = new()
        {
            ["admin"] = new() { "*:*" },
            ["user"] = new() { "read:*" },
            ["guest"] = new() { "read:public" }
        };

        public bool HasPermission(string actor, string resource, string action)
        {
            if (!_roles.TryGetValue(actor, out var permissions)) return false;
            return permissions.Any(p => p == "*:*" || p == $"{action}:*" || p == $"{action}:{resource}");
        }

        public Task<bool> HasPermissionAsync(string actor, string resource, string action, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(HasPermission(actor, resource, action));
        }
    }
}
