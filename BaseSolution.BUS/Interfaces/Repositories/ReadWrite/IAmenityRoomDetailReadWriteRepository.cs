using BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Application.Interfaces.Repositories.ReadWrite
{
    public interface IAmenityRoomDetailReadWriteRepository
    {
        Task<RequestResult<Guid>> AddAmenityRoomDetailAsync(AmenityRoomDetailEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateAmenityRoomDetailAsync(AmenityRoomDetailEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteAmenityRoomDetailAsync(AmenityRoomDetailDeleteRequest request, CancellationToken cancellationToken);
        Task<RequestResult<int>> CreateUpdateDeleteAmenityRoomDetailAsync( List<AmenityCreateUpdateDeleteRequest> request, CancellationToken cancellationToken);
    }
}
