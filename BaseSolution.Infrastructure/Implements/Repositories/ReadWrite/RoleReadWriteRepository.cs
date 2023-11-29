using BaseSolution.Application.DataTransferObjects.Role.Request;
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
    public class RoleReadWriteRepository : IRoleReadWriteRepository
    {
        private readonly AppReadWriteDbContext _appReadWriteDbContext;
        private readonly ILocalizationService _localizationService;
        public RoleReadWriteRepository(AppReadWriteDbContext appReadWriteDbContext, ILocalizationService localizationService)
        {
            _appReadWriteDbContext = appReadWriteDbContext;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<Guid>> AddRoleAsync(UserRoleEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.Id = Guid.NewGuid();
               
                entity.CreatedTime = DateTimeOffset.Now;

                await _appReadWriteDbContext.UserRoles.AddAsync(entity);
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
                        FieldName = LocalizationString.Common.FailedToCreate + "RoomBooking"
                    }
                });
            }
        }
        private async Task<UserRoleEntity?> GetUserRoleByIdAsync(Guid idRole, CancellationToken cancellationToken)
        {
            var role = await _appReadWriteDbContext.UserRoles.FirstOrDefaultAsync(x => x.Id == idRole && !x.Deleted, cancellationToken);
            return role;
        }
        public async Task<RequestResult<int>> DeleteRoleAsync(RoleDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await GetUserRoleByIdAsync(request.Id, cancellationToken);
                role!.Deleted = true;
              //  role.DeletedBy = request.DeletedBy;
                role.DeletedTime = DateTimeOffset.Now;
                role.Status = EntityStatus.Deleted;
                _appReadWriteDbContext.UserRoles.Update(role);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {

                return RequestResult<int>.Fail(_localizationService["Unable to delete role"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "role"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateRoleAsync(UserRoleEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var role = await GetUserRoleByIdAsync(entity.Id, cancellationToken);
                role!.Name = string.IsNullOrEmpty(entity.Name) ? role.Name : entity.Name;
                role.RoleCode = entity.RoleCode;
                role!.Status = entity.Status == EntityStatus.Active ? EntityStatus.Active : EntityStatus.InActive;
                role.ModifiedBy = entity.ModifiedBy;
                role.ModifiedTime = DateTimeOffset.Now;
                _appReadWriteDbContext.UserRoles.Update(role);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {

                return RequestResult<int>.Fail(_localizationService["Unable to update role"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "role"
                    }
                });
            }
        }
    }
}
