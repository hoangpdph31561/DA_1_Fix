using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Application.DataTransferObjects.User;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.DataTransferObjects.User.Request;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IUserReadOnlyRepository
    {
        Task<RequestResult<UserDTO?>> GetUserByIdAsync(Guid idUser, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<UserDTO>>> GetUserWithPaginationByAdminAsync(ViewUserWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<UserDTO>> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken);
    }
}
