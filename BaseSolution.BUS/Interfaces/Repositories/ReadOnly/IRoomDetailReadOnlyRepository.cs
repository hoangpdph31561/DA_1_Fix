using BaseSolution.Application.DataTransferObjects.RoomDetail.Request;
using BaseSolution.Application.DataTransferObjects.RoomDetail;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Application.ValueObjects.Pagination;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IRoomDetailReadOnlyRepository 
    {
        Task<RequestResult<RoomDetailDto?>> GetRoomDetailByIdAsync(Guid idRoomDetail, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<RoomDetailDto>>> GetRoomDetailWithPaginationByAdminAsync(
            ViewRoomDetailWithPaginationRequest request, CancellationToken cancellationToken);
         Task<RequestResult<PaginationResponse<RoomDetailDto>>> GetRoomDetailWithPaginationByStatusAsync(
            ViewRoomDetailWithPaginationRequest request, CancellationToken cancellationToken);

    }
}
