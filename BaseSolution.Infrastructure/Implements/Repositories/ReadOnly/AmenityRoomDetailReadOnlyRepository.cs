using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseSolution.Application.DataTransferObjects.AmenityRoomDetail;
using BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request;
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
    public class AmenityRoomDetailReadOnlyRepository : IAmenityRoomDetailReadOnlyRepository
    {
        private readonly AppReadOnlyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public AmenityRoomDetailReadOnlyRepository(AppReadOnlyDbContext appReadOnlyDbContext, IMapper mapper, ILocalizationService localizationService)
        {
            _dbContext = appReadOnlyDbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<PaginationResponse<AmenityRoomDetailDTO>>> GetAmenityByAmenityId(ViewAmenityRoomDetailWithPaginationRequestAndAmenityId request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _dbContext.AmenityRoomDetails.AsNoTracking().Where(x => !x.Deleted && x.AmenityId == request.AmenityId && !x.RoomType.Deleted).PaginateAsync<AmenityRoomDetailEntity, AmenityRoomDetailDTO>(request, _mapper, cancellationToken);
                return RequestResult<PaginationResponse<AmenityRoomDetailDTO>>.Succeed(new PaginationResponse<AmenityRoomDetailDTO>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {

                return RequestResult<PaginationResponse<AmenityRoomDetailDTO>>.Fail(_localizationService["List of AmenityRoomDetail are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error= e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of AmenityRoomDetail"
                    }
                });
            }
        }

        public async Task<RequestResult<AmenityRoomDetailDTO?>> GetAmenityRoomDetailByIdAsync(Guid idAmenityRoomDetail, CancellationToken cancellationToken)
        {
            try
            {
                var AmenityRoomDetail = await _dbContext.AmenityRoomDetails.AsNoTracking().Where(x => x.Id == idAmenityRoomDetail && !x.Deleted).ProjectTo<AmenityRoomDetailDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
                return RequestResult<AmenityRoomDetailDTO?>.Succeed(AmenityRoomDetail);

            }
            catch (Exception e)
            {

                return RequestResult<AmenityRoomDetailDTO?>.Fail(_localizationService["AmenityRoomDetail is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "AmenityRoomDetail"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<AmenityRoomDetailDTO>>> GetAmenityRoomDetailWithPaginationByAdminAsync(ViewAmenityRoomDetailWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<AmenityRoomDetailEntity> queryable = _dbContext.AmenityRoomDetails.AsNoTracking().AsQueryable().Where(x => !x.Deleted);
                if(request.RoomTypeId != Guid.Empty)
                {
                    queryable = queryable.Where(x => x.RoomTypeId == request.RoomTypeId);
                }
                var result = await queryable.PaginateAsync<AmenityRoomDetailEntity, AmenityRoomDetailDTO>(request, _mapper, cancellationToken);
                return RequestResult<PaginationResponse<AmenityRoomDetailDTO>>.Succeed(new PaginationResponse<AmenityRoomDetailDTO>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<AmenityRoomDetailDTO>>.Fail(_localizationService["List of AmenityRoomDetail are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error= e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of AmenityRoomDetail"
                    }
                });
            }
        }
    }
}
