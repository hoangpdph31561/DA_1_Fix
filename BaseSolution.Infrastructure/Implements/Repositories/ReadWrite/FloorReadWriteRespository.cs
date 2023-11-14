using BaseSolution.Application.DataTransferObjects.Floor.Request;
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
    public class FloorReadWriteRespository : IFloorReadWriteRespository
    {
        private readonly AppReadWriteDbContext _appReadWriteDbContext;
        private readonly ILocalizationService _localizationService;
        public FloorReadWriteRespository(AppReadWriteDbContext appReadWriteDbContext, ILocalizationService localizationService)
        {
            _appReadWriteDbContext = appReadWriteDbContext;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<Guid>> AddFloorAsync(FloorEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.CreatedTime = DateTimeOffset.UtcNow;

                await _appReadWriteDbContext.Floors.AddAsync(entity);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create floor"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "floor"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> DeleteFloorAsync(FloorDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed floor
                var floor = await GetFloorByIdAsync(request.Id, cancellationToken);

                // Update value to existed floor
                floor!.Deleted = true;
                floor.DeletedBy = request.DeletedBy;
                floor.DeletedTime = DateTimeOffset.UtcNow;
                floor.Status = EntityStatus.Deleted;

                _appReadWriteDbContext.Floors.Update(floor);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete floor"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "floor"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateFloorAsync(FloorEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed floor
                var floor = await GetFloorByIdAsync(entity.Id, cancellationToken);

                // Update value to existed floor
                floor!.Name = string.IsNullOrWhiteSpace(entity.Name) ? floor.Name : entity.Name;
                floor.NumberOfRoom = entity.NumberOfRoom;
                floor.BuildingId = entity.BuildingId;
                floor.ModifiedBy = entity.ModifiedBy;
                floor.Status = entity.Status;
                floor.ModifiedTime = DateTimeOffset.UtcNow;

                _appReadWriteDbContext.Floors.Update(floor);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update floor"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "floor"
                    }
                });
            }
        }
        private async Task<FloorEntity?> GetFloorByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _appReadWriteDbContext.Floors.Where(x => x.Id == id && !x.Deleted).FirstOrDefaultAsync(cancellationToken);
            return result;
        }
    }
}
