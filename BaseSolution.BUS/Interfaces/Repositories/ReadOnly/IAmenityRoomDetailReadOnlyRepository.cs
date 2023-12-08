using BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request;
using BaseSolution.Application.DataTransferObjects.AmenityRoomDetail;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Application.ValueObjects.Pagination;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IAmenityRoomDetailReadOnlyRepository
    {
        Task<RequestResult<AmenityRoomDetailDTO?>> GetAmenityRoomDetailByIdAsync(Guid idAmenityRoomDetail, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<AmenityRoomDetailDTO>>> GetAmenityRoomDetailWithPaginationByAdminAsync(ViewAmenityRoomDetailWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<AmenityRoomDetailDTO>>> GetAmenityByAmenityId(ViewAmenityRoomDetailWithPaginationRequestAndAmenityId request, CancellationToken cancellationToken);
    }
}
