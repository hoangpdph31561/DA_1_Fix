using BaseSolution.Application.DataTransferObjects.RoomType.Request;
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
    public class RoomTypeReadWriteRepository : IRoomTypeReadWriteRepository
    {
        private readonly AppReadWriteDbContext _appReadWriteDbContext;
        private readonly ILocalizationService _localizationService;
        public RoomTypeReadWriteRepository(AppReadWriteDbContext appReadWriteDbContext, ILocalizationService localizationService)
        {
            _appReadWriteDbContext = appReadWriteDbContext;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<Guid>> AddRoomTypeAsync(RoomTypeEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.CreatedTime = DateTimeOffset.UtcNow;
                await _appReadWriteDbContext.RoomTypes.AddAsync(entity);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create RoomType"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "RoomType"
                    }
                });
            }
        }

        private async Task<RoomTypeEntity?> GetRoomTypeByIdAsync(Guid idRoomType, CancellationToken cancellationToken)
        {
            var RoomType = await _appReadWriteDbContext.RoomTypes.FirstOrDefaultAsync(x => x.Id == idRoomType && !x.Deleted, cancellationToken);
            return RoomType;
        }
        public async Task<RequestResult<int>> DeleteRoomTypeAsync(RoomTypeDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var RoomType = await GetRoomTypeByIdAsync(request.Id, cancellationToken);
                RoomType!.Deleted = true;
                RoomType.DeletedBy = request.DeletedBy;
                RoomType.DeletedTime = DateTimeOffset.UtcNow;
                RoomType.Status = EntityStatus.Deleted;
                _appReadWriteDbContext.RoomTypes.Update(RoomType);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete RoomType"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "RoomType"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateRoomTypeAsync(RoomTypeEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var RoomType = await GetRoomTypeByIdAsync(entity.Id, cancellationToken);

                RoomType!.Name = string.IsNullOrEmpty(entity.Name) ? RoomType.Name : entity.Name;
                RoomType.Description = string.IsNullOrEmpty(entity.Description) ? RoomType.Description : entity.Description;
                RoomType.Status = entity.Status;
                RoomType.ModifiedBy = entity.ModifiedBy;
                RoomType.ModifiedTime = DateTimeOffset.UtcNow;
                _appReadWriteDbContext.RoomTypes.Update(RoomType);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update RoomType"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "RoomType"
                    }
                });
            }
        }
    }
}
