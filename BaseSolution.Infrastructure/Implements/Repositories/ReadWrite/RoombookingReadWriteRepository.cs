using BaseSolution.Application.DataTransferObjects.Roombooking.Request;
using BaseSolution.Application.DataTransferObjects.RoomBooking.Request;
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
    public class RoombookingReadWriteRepository : IRoombookingReadWriteRepository
    {
        private readonly AppReadWriteDbContext _appReadWriteDbContext;
        private readonly ILocalizationService _localizationService;
        public RoombookingReadWriteRepository(AppReadWriteDbContext appReadWriteDbContext, ILocalizationService localizationService)
        {
            _appReadWriteDbContext = appReadWriteDbContext;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<Guid>> AddRoomBookingAsync(RoomBookingEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var idRoomDetail = entity.RoomBookingDetails.Select(x => x.RoomDetailId).FirstOrDefault();
                if(idRoomDetail != null)
                {
                    var roomRetail = _appReadWriteDbContext.RoomDetails.Find(idRoomDetail);
                    if(roomRetail != null)
                    {
                        roomRetail.Status = RoomStatus.Reserved;
                        _appReadWriteDbContext.Update(roomRetail);
                        await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                    }
                }
                entity.Status = entity.Status;
                entity.CustomerId = entity.CustomerId;
                entity.CreatedBy = entity.CreatedBy;
                entity.CreatedTime = DateTimeOffset.Now;
                await _appReadWriteDbContext.RoomBookings.AddAsync(entity);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {

                return RequestResult<Guid>.Fail(_localizationService["Unable to create RoomBooking"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "RoomBookin"
                    }
                });
            }
        }

        public async Task<RequestResult<Guid>> AddRoomBookingByCustomerAsync(RoomBookingEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.Status = EntityStatus.PendingForConfirmation;
                entity.CustomerId = entity.CustomerId;
                entity.CreatedBy = entity.CreatedBy;
                entity.CreatedTime = DateTimeOffset.Now;
                await _appReadWriteDbContext.RoomBookings.AddAsync(entity);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {

                return RequestResult<Guid>.Fail(_localizationService["Unable to create RoomBooking"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "RoomBookin"
                    }
                });
            }
        }
        public async Task<RequestResult<int>> DeleteRoomBookingAsync(RoombookingDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var roomBooking = await GetRoomBookingByIdAsync(request.Id, cancellationToken);
                roomBooking!.Deleted = true;
                roomBooking.DeletedBy = request.DeletedBy;
                roomBooking.DeletedTime = DateTimeOffset.Now;
                roomBooking.Status = EntityStatus.Deleted;
                _appReadWriteDbContext.RoomBookings.Update(roomBooking);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {

                return RequestResult<int>.Fail(_localizationService["Unable to delete roombooking"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "roombooking"
                    }
                });
            }
        }
        private async Task<RoomBookingEntity?> GetRoomBookingByIdAsync(Guid idRoomBooking, CancellationToken cancellationToken)
        {
            var roomBooking = await _appReadWriteDbContext.RoomBookings.FirstOrDefaultAsync(x => x.Id == idRoomBooking && !x.Deleted, cancellationToken);
            return roomBooking;
        }

        public async Task<RequestResult<int>> UpdateRoomBookingAsync(RoomBookingEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var roomBooking = await GetRoomBookingByIdAsync(entity.Id, cancellationToken);

                roomBooking.Status = entity.Status;
                roomBooking.ModifiedTime = DateTimeOffset.Now;
                _appReadWriteDbContext.RoomBookings.Update(roomBooking);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {

                return RequestResult<int>.Fail(_localizationService["Unable to update RoomBooking"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "RoomBooking"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateStatusRoomBookingAsync(RoomBookingUpdateStatusRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed RoomBooking
                var roomBooking = await GetRoomBookingByIdAsync(request.Id, cancellationToken);

                // Update value to existed RoomBooking
                roomBooking!.Status = request.Status;
                roomBooking.ModifiedTime = DateTimeOffset.UtcNow;

                _appReadWriteDbContext.RoomBookings.Update(roomBooking);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update RoomBooking"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "roomBooking"
                    }
                });
            }
        }
    }
}
