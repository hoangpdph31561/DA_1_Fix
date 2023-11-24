using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomType;
using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomType.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IRoomTypeRepo
    {
        Task<PaginationResponse<RoomTypeDTO>> GetRoomType(ViewRoomTypeWithPaginationRequest request);
        Task<RoomTypeDTO> GetRoomTypeById(Guid idRoomType);
    }
}
