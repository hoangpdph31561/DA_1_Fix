using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseSolution.Application.DataTransferObjects.Building;
using BaseSolution.Application.DataTransferObjects.Building.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using BaseSolution.Infrastructure.Database.AppDbContext;
using BaseSolution.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BaseSolution.Application.ValueObjects.Common.QueryConstant;

namespace BaseSolution.Infrastructure.Implements.Repositories.ReadOnly
{
    public class BuildingReadOnlyRespository : IBuildingReadOnlyRespository
    {
        private readonly AppReadOnlyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public BuildingReadOnlyRespository(AppReadOnlyDbContext dbContext, IMapper mapper, ILocalizationService localizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<BuildingDTO?>> GetBuildingByIdAsync(Guid idBuilding, CancellationToken cancellationToken)
        {
            try
            {
                var building = await _dbContext.Buildings.AsNoTracking().Where(x => x.Id == idBuilding && !x.Deleted).ProjectTo<BuildingDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
                return RequestResult<BuildingDTO?>.Succeed(building);

            }
            catch (Exception e)
            {

                return RequestResult<BuildingDTO?>.Fail(_localizationService["Building is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "building"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<BuildingDTO>>> GetBuildingWithPaginationByAdminAsync(ViewBuildingWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<BuildingEntity> queryable = _dbContext.Buildings.AsNoTracking().AsQueryable();

                var result = await _dbContext.Buildings.AsNoTracking().PaginateAsync<BuildingEntity, BuildingDTO>(request, _mapper, cancellationToken);
                return RequestResult<PaginationResponse<BuildingDTO>>.Succeed(new PaginationResponse<BuildingDTO>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {

                return RequestResult<PaginationResponse<BuildingDTO>>.Fail(_localizationService["List of building are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error= e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of building"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<BuildingDTO>>> GetBuildingWithPaginationByOtherAsync(ViewBuildingWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<BuildingEntity> queryable = _dbContext.Buildings.AsNoTracking().AsQueryable().Where(x => !x.Deleted);
                if(!String.IsNullOrWhiteSpace(request.Search))
                {
                    queryable = queryable.Where(x => x.Name.ToLower().Contains(request.Search.ToLower()));
                }
                var result = await queryable.PaginateAsync<BuildingEntity, BuildingDTO>(request, _mapper, cancellationToken);
                return RequestResult<PaginationResponse<BuildingDTO>>.Succeed(new PaginationResponse<BuildingDTO>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {

                return RequestResult<PaginationResponse<BuildingDTO>>.Fail(_localizationService["List of building are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error= e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of building"
                    }
                });
            }
        }
    }
}
