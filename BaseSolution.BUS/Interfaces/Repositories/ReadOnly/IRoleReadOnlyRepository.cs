using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Application.DataTransferObjects.Role;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.DataTransferObjects.Role.Request;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IRoleReadOnlyRepository
    {
        Task<RequestResult<RoleDTO?>> GetRoleByIdAsync(Guid idRole, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<RoleDTO>>> GetRoleWithPaginationByAdminAsync(ViewRoleWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
