using BaseSolution.BlazorServer.Data.DataTransferObjects.Role;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Role.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IRoleRespo
    {
        Task<PaginationResponse<RoleDto>> GetAllRole(ViewRoleWithPaginationRequest request);

    }
}
