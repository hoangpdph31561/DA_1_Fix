using BaseSolution.Application.DataTransferObjects.Example.Request;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Application.Interfaces.Repositories.ReadWrite
{
    public interface IExampleReadWriteRepository
    {
        Task<RequestResult<Guid>> AddExampleAsync(ExampleEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateExampleAsync(ExampleEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteExampleAsync(ExampleDeleteRequest request, CancellationToken cancellationToken);
    }
}