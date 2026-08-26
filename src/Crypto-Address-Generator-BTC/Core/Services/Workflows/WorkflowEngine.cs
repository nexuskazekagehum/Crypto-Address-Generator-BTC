using CryptoAddressGeneratorBTC.Core.Models;

namespace CryptoAddressGeneratorBTC.Core.Services.Workflows
{
    public interface IWorkflowStep<TContext>
    {
        string Name { get; }
        Task<Result<TContext>> ExecuteAsync(TContext context, CancellationToken cancellationToken = default);
    }

    public interface IWorkflowEngine<TContext>
    {
        IWorkflowEngine<TContext> AddStep(IWorkflowStep<TContext> step);
        Task<Result<TContext>> ExecuteAsync(TContext context, CancellationToken cancellationToken = default);
    }

    public class WorkflowEngine<TContext> : IWorkflowEngine<TContext>
    {
        private readonly List<IWorkflowStep<TContext>> _steps = new();

        public IWorkflowEngine<TContext> AddStep(IWorkflowStep<TContext> step)
        {
            _steps.Add(step);
            return this;
        }

        public async Task<Result<TContext>> ExecuteAsync(TContext context, CancellationToken cancellationToken = default)
        {
            var current = Result<TContext>.Success(context);
            foreach (var step in _steps)
            {
                if (current.IsFailure) return current;
                current = await step.ExecuteAsync(current.Value!, cancellationToken);
            }
            return current;
        }
    }
}
