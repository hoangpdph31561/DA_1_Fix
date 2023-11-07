using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseSolution.Application.DataTransferObjects.Amenity;
using BaseSolution.Application.DataTransferObjects.Role;
using BaseSolution.Application.DataTransferObjects.Role.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using BaseSolution.Infrastructure.Database.AppDbContext;
using BaseSolution.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BaseSolution.Infrastructure.Implements.Repositories.ReadOnly
{
    public class RoleReadOnlyRepository : IRoleReadOnlyRepository
    {
        private readonly AppReadOnlyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public RoleReadOnlyRepository(AppReadOnlyDbContext appReadOnlyDbContext, IMapper mapper, ILocalizationService localizationService)
        {
            _dbContext = appReadOnlyDbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<RoleDTO?>> GetRoleByIdAsync(Guid idRole, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _dbContext.UserRoles.AsNoTracking().Where(x => x.Id == idRole && !x.Deleted).ProjectTo<RoleDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
                return RequestResult<RoleDTO?>.Succeed(role);

            }
            catch (Exception e)
            {

                return RequestResult<RoleDTO?>.Fail(_localizationService["Role is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "Role"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<RoleDTO>>> GetRoleWithPaginationByAdminAsync(ViewRoleWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<UserRoleEntity> queryable = _dbContext.UserRoles.AsNoTracking().AsQueryable();
                var result = await _dbContext.UserRoles.AsNoTracking().PaginateAsync<UserRoleEntity, RoleDTO>(request, _mapper, cancellationToken);
                return RequestResult<PaginationResponse<RoleDTO>>.Succeed(new PaginationResponse<RoleDTO>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {

                return RequestResult<PaginationResponse<RoleDTO>>.Fail(_localizationService["List of role are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error= e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of role"
                    }
                });
            }
        }
    }
}
