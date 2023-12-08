using BaseSolution.Application.DataTransferObjects.RoomDetail.Request;
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
    public class RoomDetailReadWriteRepository : IRoomDetailReadWriteRepository
    {
        private readonly AppReadWriteDbContext _dbContext;
        private readonly ILocalizationService _localizationService;

        public RoomDetailReadWriteRepository(ILocalizationService localizationService, AppReadWriteDbContext dbContext)
        {
            _localizationService = localizationService;
            _dbContext = dbContext;
        }
        public async Task<RequestResult<Guid>> AddRoomDetailAsync(RoomDetailEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.CreatedTime = DateTimeOffset.UtcNow;

                await _dbContext.RoomDetails.AddAsync(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create RoomDetail"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "RoomDetail"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> DeleteRoomDetailAsync(RoomDetailDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed roomDetail
                var RoomDetail = await GetRoomDetailByIdAsync(request.Id, cancellationToken);

                // Update value to existed roomDetail
                RoomDetail!.Deleted = true;
                RoomDetail.DeletedBy = request.DeletedBy;
                RoomDetail.DeletedTime = DateTimeOffset.UtcNow;
                RoomDetail.Status = RoomStatus.Deleted;

                _dbContext.RoomDetails.Update(RoomDetail);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete RoomDetail"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "RoomDetail"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateRoomDetailAsync(RoomDetailEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed roomDetail
                var roomDetail = await GetRoomDetailByIdAsync(entity.Id, cancellationToken);

                // Update value to existed roomDetail
                roomDetail!.Name = entity.Name;
                roomDetail.Price = entity.Price;
                roomDetail.Description = entity.Description;
                roomDetail.MaxPeopleStay = entity.MaxPeopleStay;
                roomDetail.RoomSize = entity.RoomSize;
                roomDetail.RoomTypeId = entity.RoomTypeId;
                roomDetail.FloorId = entity.FloorId;
                roomDetail.Images = entity.Images;
                roomDetail.Status = entity.Status;
                roomDetail.ModifiedBy = entity.ModifiedBy;
                roomDetail.ModifiedTime = DateTimeOffset.UtcNow;

                _dbContext.RoomDetails.Update(roomDetail);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update RoomDetail"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "RoomDetail"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateStatusRoomDetailAsync(RoomDetailUpdateStatusRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed roomDetail
                var roomDetail = await GetRoomDetailByIdAsync(request.Id, cancellationToken);

                // Update value to existed roomDetail

                roomDetail!.Status = request.Status;
                roomDetail.ModifiedBy = (request.ModifiedBy == Guid.Empty) ? roomDetail.ModifiedBy : request.ModifiedBy;
                roomDetail.ModifiedTime = DateTimeOffset.Now;

                _dbContext.RoomDetails.Update(roomDetail);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update RoomDetail"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "RoomDetail"
                    }
                });
            }
        }

        private async Task<RoomDetailEntity?> GetRoomDetailByIdAsync(Guid idRoomDetail, CancellationToken cancellationToken)
        {
            var roomDetail = await _dbContext.RoomDetails.FirstOrDefaultAsync(c => c.Id == idRoomDetail && !c.Deleted, cancellationToken);

            return roomDetail;
        }
    }
}
