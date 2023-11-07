using BaseSolution.Application.DataTransferObjects.RoomBookingDetail.Request;
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
    public class RoomBookingDetailReadWriteRepository : IRoomBookingDetailReadWriteRepository
    {
        private readonly AppReadWriteDbContext _appReadWriteDbContext;
        private readonly ILocalizationService _localizationService;
        public RoomBookingDetailReadWriteRepository(AppReadWriteDbContext appReadWriteDbContext, ILocalizationService localizationService)
        {
            _appReadWriteDbContext = appReadWriteDbContext;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<Guid>> AddRoomBookingDetailAsync(RoomBookingDetailEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.Id = Guid.NewGuid();

                entity.Status = entity.Status == EntityStatus.Active ? EntityStatus.Active : EntityStatus.InActive;
                entity.CreatedTime = DateTimeOffset.Now;
                await _appReadWriteDbContext.RoomBookingDetails.AddAsync(entity);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create RoomBookingDetail"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "RoomBookingDetail"
                    }
                });
            }
        }
        private async Task<RoomBookingDetailEntity?> GetRoomBookingDetailByIdAsync(Guid idRoomBookingDetail, CancellationToken cancellationToken)
        {
            var roomBookingDetail = await _appReadWriteDbContext.RoomBookingDetails.FirstOrDefaultAsync(x => x.Id == idRoomBookingDetail && !x.Deleted, cancellationToken);
            return roomBookingDetail;
        }
        public async Task<RequestResult<int>> DeleteRoomBookingDetailAsync(RoomBookingDetailDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var roomBooking = await GetRoomBookingDetailByIdAsync(request.Id, cancellationToken);
                roomBooking!.Deleted = true;
                roomBooking.DeletedBy = request.DeletedBy;
                roomBooking.DeletedTime = DateTimeOffset.Now;
                roomBooking.Status = EntityStatus.Deleted;
                _appReadWriteDbContext.RoomBookingDetails.Update(roomBooking);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {

                return RequestResult<int>.Fail(_localizationService["Unable to delete RoomBookingDetail"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "RoomBookingDetail"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateRoomBookingDetailAsync(RoomBookingDetailEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var roomBookingDetail = await GetRoomBookingDetailByIdAsync(entity.Id, cancellationToken);

                roomBookingDetail!.Status = entity.Status == EntityStatus.Active ? EntityStatus.Active : EntityStatus.InActive;
                roomBookingDetail.ModifiedBy = entity.ModifiedBy;
                roomBookingDetail.ModifiedTime = DateTimeOffset.Now;
                _appReadWriteDbContext.RoomBookingDetails.Update(roomBookingDetail);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {

                return RequestResult<int>.Fail(_localizationService["Unable to update RoomBookingDetail"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "RoomBookingDetail"
                    }
                });
            }
        }
    }
}
