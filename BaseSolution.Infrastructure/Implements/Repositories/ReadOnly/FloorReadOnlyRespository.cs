using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseSolution.Application.DataTransferObjects.Example;
using BaseSolution.Application.DataTransferObjects.Floor;
using BaseSolution.Application.DataTransferObjects.Floor.Request;
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
    public class FloorReadOnlyRespository : IFloorReadOnlyRespository
    {
        private readonly AppReadOnlyDbContext _appReadOnlyDbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public FloorReadOnlyRespository(AppReadOnlyDbContext appReadOnlyDbContext, IMapper mapper, ILocalizationService localizationService)
        {
            _appReadOnlyDbContext = appReadOnlyDbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<FloorDTO?>> GetFloorByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _appReadOnlyDbContext.Floors.AsNoTracking().Where(c => c.Id == id && !c.Deleted).ProjectTo<FloorDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

                return RequestResult<FloorDTO?>.Succeed(result);
            }
            catch (Exception e)
            {
                return RequestResult<FloorDTO?>.Fail(_localizationService["Floor is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "floor"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<FloorDTO>>> GetFloorWithPaginationByAdminAsync(ViewFloorWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<FloorEntity> queryable = _appReadOnlyDbContext.Floors.AsNoTracking().AsQueryable();
                
                var result = await _appReadOnlyDbContext.Floors.AsNoTracking()
                    .PaginateAsync<FloorEntity, FloorDTO>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<FloorDTO>>.Succeed(new PaginationResponse<FloorDTO>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<FloorDTO>>.Fail(_localizationService["List of Floors are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of Floors"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<FloorDTO>>> GetFloorWithPaginationByOtherAsync(ViewFloorWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<FloorEntity> queryable = _appReadOnlyDbContext.Floors.AsNoTracking().AsQueryable().Where(x=> !x.Deleted);
                if (!String.IsNullOrWhiteSpace(request.SearchString))
                {
                    queryable = queryable.Where(x => x.Name.ToLower().Contains(request.SearchString.ToLower()));
                }
                var result = await queryable.Where(x => !x.Building.Deleted && x.BuildingId == request.BuildingId)
                    .PaginateAsync<FloorEntity, FloorDTO>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<FloorDTO>>.Succeed(new PaginationResponse<FloorDTO>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<FloorDTO>>.Fail(_localizationService["List of Floors are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of Floors"
                    }
                });
            }
        }
    }
}
