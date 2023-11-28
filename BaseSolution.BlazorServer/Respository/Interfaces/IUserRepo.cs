using BaseSolution.BlazorServer.Data.DataTransferObjects.User;
using BaseSolution.BlazorServer.Data.DataTransferObjects.User.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IUserRepo
    {
        Task<bool> ConfirmAccountAsync(string username, string password);
        Task<PaginationResponse<UserHotelDTO>> GetUserAsync(ViewUsersHotelWithPaginationRequest request);
        Task<bool> CreateUserAsync(UserCreateRequest request);
        Task<bool> UpdateUserAsync(UserUpdateRequest request);
        Task<bool> DeleteUserAsync(UserDeleteRequest request);
        Task<UserHotelDTO> GetUserById(Guid id);
    }
}
