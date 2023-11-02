using BaseSolution.Application.DataTransferObjects.Example;
using BaseSolution.Application.DataTransferObjects.Example.Request;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.ValueObjects.Response;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IExampleReadOnlyRepository
    {
        Task<RequestResult<ExampleDto?>> GetExampleByIdAsync(Guid idExample, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<ExampleDto>>> GetExampleWithPaginationByAdminAsync(
            ViewExampleWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
