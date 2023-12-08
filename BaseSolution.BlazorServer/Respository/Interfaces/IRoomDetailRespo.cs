using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomDetail;
using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomDetail.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IRoomDetailRespo
    {
        Task<PaginationResponse<RoomDetailDTO>> GetAllRoomDetails(ViewRoomDetailWithPaginationRequest request);

        Task<PaginationResponse<RoomDetailDTO>> GetAllRoomDetailsByStatus(ViewRoomDetailByCheckInCheckOutRequest request);

        Task<RoomDetailDTO> GetRoomDetailById(Guid id);
        Task<List<RoomDetailDTO>> GetRoomDetailByIdRoomType(Guid id);
        Task<bool> CreateNewRoomDetail(RoomDetailCreateRequest request);
        Task<bool> UpdateRoomDetail(RoomDetailUpdateRequest request);
        Task<bool> DeleteRoomDetail(RoomDetailDeleteRequest request);
        Task<bool> UpdateRoomDetailStatus(RoomDetailUpdateStatusRequest request);
    }
}
