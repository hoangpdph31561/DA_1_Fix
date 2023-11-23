using BaseSolution.Application.DataTransferObjects.Building.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using BaseSolution.Domain.Enums;
using BaseSolution.Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Implements.Repositories.ReadWrite
{
    public class BuildingReadWriteRespository : IBuildingReadWriteRespository
    {
        private readonly AppReadWriteDbContext _appReadWriteDbContext;
        private readonly ILocalizationService _localizationService;
        public BuildingReadWriteRespository(AppReadWriteDbContext appReadWriteDbContext, ILocalizationService localizationService)
        {
            _appReadWriteDbContext = appReadWriteDbContext;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<Guid>> AddBuildingAsync(BuildingEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.CreatedTime = DateTimeOffset.UtcNow;
                await _appReadWriteDbContext.Buildings.AddAsync(entity);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {

                return RequestResult<Guid>.Fail(_localizationService["Unable to create Building"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "building"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> DeleteBuildingAsync(BuildingDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var building = await GetBuildingByIdAsync(request.Id, cancellationToken);
                building!.Deleted = true;
                building.DeletedBy = request.DeletedBy;
                building.DeletedTime = DateTimeOffset.UtcNow;
                building.Status = EntityStatus.Deleted;
                _appReadWriteDbContext.Buildings.Update(building);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {

                return RequestResult<int>.Fail(_localizationService["Unable to delete building"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "building"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateBuildingAsync(BuildingEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var building = await GetBuildingByIdAsync(entity.Id, cancellationToken);
                building!.Name = string.IsNullOrEmpty(entity.Name) ? building.Name : entity.Name;
                building.Status = entity.Status;
                building.ModifiedBy = entity.ModifiedBy;
                building.ModifiedTime = DateTimeOffset.UtcNow;
                _appReadWriteDbContext.Buildings.Update(building);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {

                return RequestResult<int>.Fail(_localizationService["Unable to update building"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "building"
                    }
                });
            }
        }
        private async Task<BuildingEntity?> GetBuildingByIdAsync (Guid id, CancellationToken cancellationToken)
        {
            var building = await _appReadWriteDbContext.Buildings.FirstOrDefaultAsync(x => x.Id == id && !x.Deleted, cancellationToken);
            return building;
        }
    }
}
