using BaseSolution.Application.DataTransferObjects.Amenity.Request;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Application.Interfaces.Repositories.ReadWrite
{
    public interface IAmenityReadWriteRepository
    {
        Task<RequestResult<Guid>> AddAmenityAsync(AmenityEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateAmenityAsync(AmenityEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteAmenityAsync(AmenityDeleteRequest request, CancellationToken cancellationToken);
    }
}
