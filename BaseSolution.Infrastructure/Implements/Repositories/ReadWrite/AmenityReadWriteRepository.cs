using BaseSolution.Application.DataTransferObjects.Amenity.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using BaseSolution.Domain.Enums;
using BaseSolution.Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace BaseSolution.Infrastructure.Implements.Repositories.ReadWrite
{
    public class AmenityReadWriteRepository : IAmenityReadWriteRepository
    {
        private readonly AppReadWriteDbContext _appReadWriteDbContext;
        private readonly ILocalizationService _localizationService;
        public AmenityReadWriteRepository(AppReadWriteDbContext appReadWriteDbContext, ILocalizationService localizationService)
        {
            _appReadWriteDbContext = appReadWriteDbContext;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<Guid>> AddAmenityAsync(AmenityEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.CreatedTime = DateTimeOffset.UtcNow;
                await _appReadWriteDbContext.Amenities.AddAsync(entity);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create Amenity"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "Amenity"
                    }
                });
            }
        }

        private async Task<AmenityEntity?> GetAmenityByIdAsync(Guid idAmenity, CancellationToken cancellationToken)
        {
            var amenity = await _appReadWriteDbContext.Amenities.FirstOrDefaultAsync(x => x.Id == idAmenity && !x.Deleted, cancellationToken);
            return amenity;
        }
        public async Task<RequestResult<int>> DeleteAmenityAsync(AmenityDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var amenity = await GetAmenityByIdAsync(request.Id, cancellationToken);
                amenity!.Deleted = true;
                amenity.DeletedBy = request.DeletedBy;
                amenity.DeletedTime = DateTimeOffset.UtcNow;
                amenity.Status = EntityStatus.Deleted;
                _appReadWriteDbContext.Amenities.Update(amenity);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete amenity"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "amenity"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateAmenityAsync(AmenityEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var amenity = await GetAmenityByIdAsync(entity.Id, cancellationToken);

                amenity!.Name = string.IsNullOrEmpty(entity.Name) ? amenity.Name : entity.Name;
                amenity.Description = string.IsNullOrEmpty(entity.Description) ? amenity.Description : entity.Description;
                amenity.Status = entity.Status == EntityStatus.Active ? EntityStatus.Active : EntityStatus.InActive;
                amenity.ModifiedBy = entity.ModifiedBy;
                amenity.ModifiedTime = DateTimeOffset.UtcNow;
                _appReadWriteDbContext.Amenities.Update(amenity);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update amenity"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "amenity"
                    }
                });
            }
        }
    }
}
