using BaseSolution.Application.DataTransferObjects.User.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using BaseSolution.Domain.Enums;
using BaseSolution.Infrastructure.Database.AppDbContext;
using BaseSolution.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BaseSolution.Infrastructure.Implements.Repositories.ReadWrite
{
    public class UserReadWriteRepository : IUserReadWriteRepository
    {

        private readonly AppReadWriteDbContext _appReadWriteDbContext;
        private readonly ILocalizationService _localizationService;
        public UserReadWriteRepository(AppReadWriteDbContext appReadWriteDbContext, ILocalizationService localizationService)
        {
            _appReadWriteDbContext = appReadWriteDbContext;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<Guid>> AddUserAsync(UserEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.Id = Guid.NewGuid();
                entity.UserName = entity.UserName;
                entity.Password = UtilityExtensions.GenerateRandomString(6);
                entity.Email = entity.Email;
                entity.PhoneNumber = entity.PhoneNumber;
                entity.Status = entity.Status;
                entity.CreatedBy = entity.CreatedBy;
                entity.CreatedTime = DateTimeOffset.Now;
                await _appReadWriteDbContext.Users.AddAsync(entity);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {

                return RequestResult<Guid>.Fail(_localizationService["Unable to create User"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "User"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> DeleteUserAsync(UserDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await GetUserByIdAsync(request.Id, cancellationToken);
                user!.Deleted = true;
                user.DeletedBy = request.DeletedBy;
                user.DeletedTime = DateTimeOffset.Now;
                user.Status = EntityStatus.Deleted;
                _appReadWriteDbContext.Users.Update(user);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete user"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "user"
                    }
                });
            }
        }
        public async Task<RequestResult<int>> UpdateUserAsync(UserEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var user = await GetUserByIdAsync(entity.Id, cancellationToken);
                user!.UserRoleId = entity.UserRoleId;
                user.Status = entity.Status;
                user.PhoneNumber = entity.PhoneNumber;
                user.Email = entity.Email;
                user.ModifiedTime = DateTimeOffset.Now;
                _appReadWriteDbContext.Users.Update(user);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {

                return RequestResult<int>.Fail(_localizationService["Unable to update user"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "user"
                    }
                });
            }
        }
        private async Task<UserEntity?> GetUserByIdAsync(Guid idUser, CancellationToken cancellationToken)
        {
            var user = await _appReadWriteDbContext.Users.FirstOrDefaultAsync(x => x.Id == idUser && !x.Deleted, cancellationToken);
            return user;
        }
    }
}
