using BaseSolution.BlazorServer.Data.DataTransferObjects.User;
using BaseSolution.BlazorServer.Data.DataTransferObjects.User.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IUserRepo
    {
        Task<bool> ConfirmAccountAsync(string username, string password);
        Task<PaginationResponse<UserHotelDTO>> GetAllUser(ViewUsersHotelWithPaginationRequest request);
        Task<bool> CreateNewUser(UserHotelCreateRequest request);
        Task<bool> UpdateUser(UserHotelUpdateRequest request);
        Task<bool> DeleteUser(UserHotelDeleteRequest request);
        Task<UserHotelDTO> GetUserById(Guid id);
    }
}
