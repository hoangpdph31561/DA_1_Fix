using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseSolution.Application.DataTransferObjects.RoomType;
using BaseSolution.Application.DataTransferObjects.RoomType.Request;
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
    public class RoomTypeReadOnlyRepository : IRoomTypeReadOnlyRepository
    {
        private readonly AppReadOnlyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public RoomTypeReadOnlyRepository(AppReadOnlyDbContext appReadOnlyDbContext, IMapper mapper, ILocalizationService localizationService)
        {
            _dbContext = appReadOnlyDbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<RoomTypeDTO?>> GetRoomTypeByIdAsync(Guid idRoomType, CancellationToken cancellationToken)
        {
            try
            {
                var RoomType = await _dbContext.RoomTypes.AsNoTracking().Where(x => x.Id == idRoomType && !x.Deleted).ProjectTo<RoomTypeDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
                return RequestResult<RoomTypeDTO?>.Succeed(RoomType);

            }
            catch (Exception e)
            {

                return RequestResult<RoomTypeDTO?>.Fail(_localizationService["RoomType is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "RoomType"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<RoomTypeDTO>>> GetRoomTypeWithPaginationByAdminAsync(ViewRoomTypeWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<RoomTypeEntity> queryable = _dbContext.RoomTypes.AsNoTracking().AsQueryable().Where(x => !x.Deleted);
                if(!string.IsNullOrWhiteSpace(request.SearchString))
                {
                    queryable = queryable.Where(x => x.Name.ToLower().Trim().Contains(request.SearchString.ToLower().Trim()));
                }
                var result = await queryable.PaginateAsync<RoomTypeEntity, RoomTypeDTO>(request, _mapper, cancellationToken);
                return RequestResult<PaginationResponse<RoomTypeDTO>>.Succeed(new PaginationResponse<RoomTypeDTO>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<RoomTypeDTO>>.Fail(_localizationService["List of RoomType are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error= e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of RoomType"
                    }
                });
            }
        }
    }
}
