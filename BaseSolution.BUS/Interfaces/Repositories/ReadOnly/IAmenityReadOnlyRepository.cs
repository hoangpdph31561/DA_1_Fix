using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Application.DataTransferObjects.Amenity;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.DataTransferObjects.Amenity.Request;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IAmenityReadOnlyRepository
    {
        Task<RequestResult<AmenityDTO?>> GetAmenityByIdAsync(Guid idAmenity, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<AmenityDTO>>> GetAmenityWithPaginationByAdminAsync(ViewAmenityWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
