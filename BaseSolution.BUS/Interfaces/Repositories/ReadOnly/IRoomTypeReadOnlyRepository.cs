using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Application.DataTransferObjects.RoomType;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.DataTransferObjects.RoomType.Request;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IRoomTypeReadOnlyRepository
    {
        Task<RequestResult<RoomTypeDTO?>> GetRoomTypeByIdAsync(Guid idRoomType, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<RoomTypeDTO>>> GetRoomTypeWithPaginationByAdminAsync(ViewRoomTypeWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
