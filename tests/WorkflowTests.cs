using CryptoAddressGeneratorBTC.Core.Models;
using CryptoAddressGeneratorBTC.Core.Services.Workflows;
using Xunit;

namespace CryptoAddressGeneratorBTC.Tests
{
    public class WorkflowTests
    {
        [Fact]
        public async Task WorkflowEngine_RunsAllSteps()
        {
            var engine = new WorkflowEngine<int>()
                .AddStep(new AddStep(1))
                .AddStep(new AddStep(2));

            var result = await engine.ExecuteAsync(0);
            Assert.True(result.IsSuccess);
            Assert.Equal(3, result.Value);
        }

        [Fact]
        public async Task WorkflowEngine_StopsOnFailure()
        {
            var engine = new WorkflowEngine<int>()
                .AddStep(new FailingStep());

            var result = await engine.ExecuteAsync(0);
            Assert.True(result.IsFailure);
        }

        private class AddStep : IWorkflowStep<int>
        {
            public string Name => nameof(AddStep);
            private readonly int _value;

            public AddStep(int value) { _value = value; }

            public Task<Result<int>> ExecuteAsync(int context, CancellationToken cancellationToken = default)
            {
                return Task.FromResult(Result<int>.Success(context + _value));
            }
        }

        private class FailingStep : IWorkflowStep<int>
        {
            public string Name => nameof(FailingStep);
            public Task<Result<int>> ExecuteAsync(int context, CancellationToken cancellationToken = default)
            {
                return Task.FromResult(Result<int>.Failure("Intentional failure"));
            }
        }
    }
}
