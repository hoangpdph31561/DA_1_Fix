using BaseSolution.Application.DataTransferObjects.Role.Request;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Application.Interfaces.Repositories.ReadWrite
{
    public interface IRoleReadWriteRepository
    {
        Task<RequestResult<Guid>> AddRoleAsync(UserRoleEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateRoleAsync(UserRoleEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteRoleAsync(RoleDeleteRequest request, CancellationToken cancellationToken);
    }
}
