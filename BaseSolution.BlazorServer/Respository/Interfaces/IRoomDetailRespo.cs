using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomDetail;
using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomDetail.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IRoomDetailRespo
    {
        Task<PaginationResponse<RoomDetailDTO>> GetAllRoomDetails(ViewRoomDetailWithPaginationRequest request);
    }
}
