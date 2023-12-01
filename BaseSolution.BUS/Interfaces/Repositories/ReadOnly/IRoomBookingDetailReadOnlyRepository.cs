using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Application.DataTransferObjects.RoomBookingDetail;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.DataTransferObjects.RoomBookingDetail.Request;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IRoomBookingDetailReadOnlyRepository
    {
        Task<RequestResult<RoomBookingDetailDTO?>> GetRoomBookingDetailByIdAsync(Guid idRoomBookingDetail, CancellationToken cancellationToken);
        Task<RequestResult<RoomBookingDetailDTO?>> GetRoomBookingDetailByIdRoomBookingAsync(Guid idRoomBooking, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<RoomBookingDetailDTO>>> GetRoomBookingDetailWithPaginationByAdminAsync(ViewRoomBookingDetailWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<RoomBookingDetailDTO>>> GetRoomBookingDetailWithPaginationByOtherAsync(ViewRoomBookingDetailWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<RoomBookingDetailDTO>> GetRoomBookingDetailWithPaginationByIdRoomBookingAsync(Guid idRoomBooking, CancellationToken cancellationToken);

    }
}
