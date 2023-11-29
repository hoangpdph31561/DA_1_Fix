using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseSolution.Application.DataTransferObjects.User;
using BaseSolution.Application.DataTransferObjects.User.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using BaseSolution.Domain.Enums;
using BaseSolution.Infrastructure.Database.AppDbContext;
using BaseSolution.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
namespace BaseSolution.Infrastructure.Implements.Repositories.ReadOnly
{
    public class UserReadOnlyRepository : IUserReadOnlyRepository
    {
        private readonly AppReadOnlyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public UserReadOnlyRepository(AppReadOnlyDbContext dbContext, IMapper mapper, ILocalizationService localizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<UserDTO?>> GetUserByIdAsync(Guid idUser, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _dbContext.Users.AsNoTracking().Where(x => x.Id == idUser).ProjectTo<UserDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
                return RequestResult<UserDTO?>.Succeed(user);

            }
            catch (Exception e)
            {

                return RequestResult<UserDTO?>.Fail(_localizationService["User is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "user"
                    }
                });
            }
        }

        public async Task<RequestResult<UserDTO>> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _dbContext.Users.AsNoTracking().Where(x => x.UserName == userName).ProjectTo<UserDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
                return RequestResult<UserDTO>.Succeed(user);

            }
            catch (Exception e)
            {

                return RequestResult<UserDTO>.Fail(_localizationService["User is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "user"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<UserDTO>>> GetUserWithPaginationByAdminAsync(ViewUserWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user =  _dbContext.Users.AsNoTracking().Where(x => x.Status != EntityStatus.Deleted).ProjectTo<UserDTO>(_mapper.ConfigurationProvider);
                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    user = user.Where(x => x.Name.Contains(request.Name));
                }
                if(request.UserRoleId != null) 
                {
                    user = user.Where(x => x.UserRoleId == request.UserRoleId);
                }
                var result = await user.PaginateAsync(request, cancellationToken);
                return RequestResult<PaginationResponse<UserDTO>>.Succeed(new PaginationResponse<UserDTO>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {

                return RequestResult<PaginationResponse<UserDTO>>.Fail(_localizationService["List of user are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error= e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of user"
                    }
                });
            }
        }
    }
}
