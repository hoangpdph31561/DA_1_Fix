using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomType;
using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomType.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IRoomTypeRespo
    {
        Task<PaginationResponse<RoomTypeDTO>> GetAllRoomTypes(ViewRoomTypeWithPaginationRequest request);
        Task<bool> CreateNewRoomType(RoomTypeCreateRequest request);
        Task<bool> UpdateRoomType(RoomTypeUpdateRequest request);
        Task<bool> DeleteRoomType(RoomTypeDeleteRequest request);
        Task<RoomTypeDTO> GetRoomTypeById(Guid id);
    }
}
