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
                entity.Password = entity.Password;
                entity.Email = entity.Email;
                entity.PhoneNumber = entity.PhoneNumber;
                entity.Status = entity.Status == EntityStatus.Active ? EntityStatus.Active : EntityStatus.InActive;
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

        public async Task<RequestResult<int>> UpdateUserAsync(UserEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var user = await GetUserByIdAsync(entity.Id, cancellationToken);

                user!.UserRole = entity.UserRole;
                user.Status = entity.Status == EntityStatus.Active ? EntityStatus.Active : EntityStatus.InActive;
                user.ModifiedBy = entity.ModifiedBy;
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
