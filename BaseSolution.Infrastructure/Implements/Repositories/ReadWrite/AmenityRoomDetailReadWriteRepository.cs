using BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request;
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
    public class AmenityRoomDetailReadWriteRepository : IAmenityRoomDetailReadWriteRepository
    {
        private readonly AppReadWriteDbContext _appReadWriteDbContext;
        private readonly ILocalizationService _localizationService;
        public AmenityRoomDetailReadWriteRepository(AppReadWriteDbContext appReadWriteDbContext, ILocalizationService localizationService)
        {
            _appReadWriteDbContext = appReadWriteDbContext;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<Guid>> AddAmenityRoomDetailAsync(AmenityRoomDetailEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.CreatedTime = DateTimeOffset.Now;
                await _appReadWriteDbContext.AmenityRoomDetails.AddAsync(entity);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create AmenityRoomDetail"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "AmenityRoomDetail"
                    }
                });
            }
        }

        private async Task<AmenityRoomDetailEntity?> GetAmenityRoomDetailByIdAsync(Guid idAmenityRoomDetail, CancellationToken cancellationToken)
        {
            var AmenityRoomDetail = await _appReadWriteDbContext.AmenityRoomDetails.FirstOrDefaultAsync(x => x.Id == idAmenityRoomDetail && !x.Deleted, cancellationToken);
            return AmenityRoomDetail;
        }
        public async Task<RequestResult<int>> DeleteAmenityRoomDetailAsync(AmenityRoomDetailDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var AmenityRoomDetail = await GetAmenityRoomDetailByIdAsync(request.Id, cancellationToken);
                AmenityRoomDetail!.Deleted = true;
                AmenityRoomDetail.DeletedBy = request.DeletedBy;
                AmenityRoomDetail.DeletedTime = DateTimeOffset.Now;
                AmenityRoomDetail.Status = EntityStatus.Deleted;
                _appReadWriteDbContext.AmenityRoomDetails.Update(AmenityRoomDetail);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete AmenityRoomDetail"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "AmenityRoomDetail"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateAmenityRoomDetailAsync(AmenityRoomDetailEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var AmenityRoomDetail = await GetAmenityRoomDetailByIdAsync(entity.Id, cancellationToken);
                AmenityRoomDetail!.AmenityId = entity.AmenityId;
                AmenityRoomDetail.RoomTypeId = entity.RoomTypeId;
                AmenityRoomDetail!.Amount = entity.Amount;
                AmenityRoomDetail.Status = entity.Status == EntityStatus.Active ? EntityStatus.Active : EntityStatus.InActive;
                AmenityRoomDetail.ModifiedBy = entity.ModifiedBy;
                AmenityRoomDetail.ModifiedTime = DateTimeOffset.Now;
                _appReadWriteDbContext.AmenityRoomDetails.Update(AmenityRoomDetail);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update AmenityRoomDetail"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "AmenityRoomDetail"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> CreateUpdateDeleteAmenityRoomDetailAsync(List<AmenityCreateUpdateDeleteRequest> request, CancellationToken cancellationToken)
        {
            try
            {
                Guid roomTypeId = request[0].RoomTypeId;
                var lstEsists = await _appReadWriteDbContext.AmenityRoomDetails.Where(x => x.RoomTypeId == roomTypeId && !x.Deleted).ToListAsync(cancellationToken);
                List<AmenityCreateUpdateDeleteRequest> lstCreate = new();
                List<AmenityCreateUpdateDeleteRequest> lstUpdate = new();
                List<AmenityCreateUpdateDeleteRequest> lstDelete = new();
                foreach (var item in request)
                {
                    if(!lstEsists.Exists(x => x.AmenityId == item.AmenityId))
                    {
                        lstCreate.Add(item);
                    }
                    if(lstEsists.Exists(x => x.AmenityId == item.AmenityId  && x.Amount != item.Amount))
                    {
                        lstUpdate.Add(item);
                    }
                }
                foreach (var item in lstEsists)
                {
                    if(!request.Exists(x => x.AmenityId == item.AmenityId))
                    {
                        lstDelete.Add(new AmenityCreateUpdateDeleteRequest
                        {
                            AmenityId = item.AmenityId,
                            Amount = item.Amount,
                        });
                    }
                }
                if(lstCreate.Any())
                {
                    foreach (var item in lstCreate)
                    {
                        AmenityRoomDetailEntity entity = new()
                        {
                            AmenityId = item.AmenityId,
                            RoomTypeId = roomTypeId,
                            Amount = item.Amount,
                            CreatedTime = DateTimeOffset.UtcNow,
                        };
                        await _appReadWriteDbContext.AmenityRoomDetails.AddAsync(entity);
                    }
                }
                if (lstUpdate.Any())
                {
                    foreach (var item in lstUpdate)
                    {
                        var update = await _appReadWriteDbContext.AmenityRoomDetails.FirstOrDefaultAsync(x => x.RoomTypeId == roomTypeId && x.AmenityId == item.AmenityId && !x.Deleted);
                        update!.Amount = item.Amount;
                        update.ModifiedTime = DateTimeOffset.UtcNow;
                        _appReadWriteDbContext.AmenityRoomDetails.Update(update);
                    }
                }
                if (lstDelete.Any())
                {
                    foreach (var item in lstDelete)
                    {
                        var delete = await _appReadWriteDbContext.AmenityRoomDetails.FirstOrDefaultAsync(x => x.RoomTypeId == roomTypeId && x.AmenityId == item.AmenityId && !x.Deleted);
                        delete!.Status = EntityStatus.Deleted;
                        delete.Deleted = true;
                        delete.DeletedTime = DateTimeOffset.UtcNow;
                        _appReadWriteDbContext.AmenityRoomDetails.Update(delete);
                    }
                }
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update AmenityRoomDetail"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "AmenityRoomDetail"
                    }
                });
            }
        }
    }
}
