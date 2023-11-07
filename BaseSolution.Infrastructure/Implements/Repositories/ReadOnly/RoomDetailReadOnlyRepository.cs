using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseSolution.Application.DataTransferObjects.RoomDetail;
using BaseSolution.Application.DataTransferObjects.RoomDetail.Request;
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

namespace BaseSolution.Infrastructure.Implements.Repositories.ReadOnly
{
    public class RoomDetailReadOnlyRepository : IRoomDetailReadOnlyRepository
    {
        private readonly DbSet<RoomDetailEntity> _RoomDetailEntities;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public RoomDetailReadOnlyRepository(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _RoomDetailEntities = dbContext.Set<RoomDetailEntity>();
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<RoomDetailDto?>> GetRoomDetailByIdAsync(Guid idRoomDetail, CancellationToken cancellationToken)
        {
            try
            {
                var RoomDetail = await _RoomDetailEntities.AsNoTracking().Where(c => c.Id == idRoomDetail && !c.Deleted).ProjectTo<RoomDetailDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

                return RequestResult<RoomDetailDto?>.Succeed(RoomDetail);
            }
            catch (Exception e)
            {
                return RequestResult<RoomDetailDto?>.Fail(_localizationService["RoomDetail is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "RoomDetail"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<RoomDetailDto>>> GetRoomDetailWithPaginationByAdminAsync(ViewRoomDetailWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<RoomDetailEntity> queryable = _RoomDetailEntities.AsNoTracking().AsQueryable();
                var result = await _RoomDetailEntities.AsNoTracking()
                    .PaginateAsync<RoomDetailEntity, RoomDetailDto>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<RoomDetailDto>>.Succeed(new PaginationResponse<RoomDetailDto>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<RoomDetailDto>>.Fail(_localizationService["List of RoomDetail are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of RoomDetail"
                    }
                });
            }
        }
    }
}
