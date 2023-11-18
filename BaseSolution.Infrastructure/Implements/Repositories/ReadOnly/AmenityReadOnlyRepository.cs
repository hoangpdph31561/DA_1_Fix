using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseSolution.Application.DataTransferObjects.Amenity;
using BaseSolution.Application.DataTransferObjects.Amenity.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using BaseSolution.Infrastructure.Database.AppDbContext;
using BaseSolution.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using static BaseSolution.Application.ValueObjects.Common.QueryConstant;

namespace BaseSolution.Infrastructure.Implements.Repositories.ReadOnly
{
    public class AmenityReadOnlyRepository : IAmenityReadOnlyRepository
    {
        private readonly AppReadOnlyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public AmenityReadOnlyRepository(AppReadOnlyDbContext appReadOnlyDbContext, IMapper mapper, ILocalizationService localizationService)
        {
            _dbContext = appReadOnlyDbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<AmenityDTO?>> GetAmenityByIdAsync(Guid idAmenity, CancellationToken cancellationToken)
        {
            try
            {
                var amenity = await _dbContext.Amenities.AsNoTracking().Where(x => x.Id == idAmenity && !x.Deleted).ProjectTo<AmenityDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
                return RequestResult<AmenityDTO?>.Succeed(amenity);

            }
            catch (Exception e)
            {

                return RequestResult<AmenityDTO?>.Fail(_localizationService["Amenity is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "Amenity"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<AmenityDTO>>> GetAmenityWithPaginationByAdminAsync(ViewAmenityWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<AmenityEntity> queryable = _dbContext.Amenities.AsNoTracking().AsQueryable().Where(x => !x.Deleted);
                if (!String.IsNullOrWhiteSpace(request.SearchString))
                {
                    queryable = queryable.Where(x => x.Name.ToLower().Contains(request.SearchString.ToLower()));
                }

                var result = await queryable.PaginateAsync<AmenityEntity, AmenityDTO>(request, _mapper, cancellationToken);
                return RequestResult<PaginationResponse<AmenityDTO>>.Succeed(new PaginationResponse<AmenityDTO>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<AmenityDTO>>.Fail(_localizationService["List of Amenity are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error= e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of Amenity"
                    }
                });
            }
        }
    }
}
