using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseSolution.Application.DataTransferObjects.RoomDetail;
using BaseSolution.Application.DataTransferObjects.RoomDetail.Request;
using BaseSolution.Application.DataTransferObjects.RoomType;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Implements.Repositories.ReadOnly
{
    public class RoomDetailReadOnlyRepository : IRoomDetailReadOnlyRepository
    {
        private readonly AppReadOnlyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public RoomDetailReadOnlyRepository(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<RoomDetailDto?>> GetRoomDetailByIdAsync(Guid idRoomDetail, CancellationToken cancellationToken)
        {
            try
            {
                var RoomDetail = await _dbContext.RoomDetails.AsNoTracking().Where(c => c.Id == idRoomDetail && !c.Deleted).ProjectTo<RoomDetailDto>(_mapper.ConfigurationProvider)
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

        public async Task<RequestResult<List<RoomDetailDto>>> GetRoomDetailByIdRoomTypeAsync(Guid idRoomType, CancellationToken cancellationToken)
        {
            try
            {
                var getList = await _dbContext.RoomDetails.ToListAsync();
                var listByType = getList.Where(c => c.RoomTypeId == idRoomType);
                var result = new List<RoomDetailDto>();
                foreach (var item in listByType)
                {
                    var data = _mapper.Map<RoomDetailDto>(item);
                    result.Add(data);
                }
                return RequestResult<List<RoomDetailDto>>.Succeed(result);
            }
            catch (Exception e)
            {
                return RequestResult<List<RoomDetailDto>>.Fail(_localizationService["RoomDetail is not found"], new[]
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
                var queryable = _dbContext.RoomDetails.AsNoTracking().AsQueryable().Where(x => !x.Deleted).ProjectTo<RoomDetailDto>(_mapper.ConfigurationProvider);
                if(request.BuildingId != Guid.Empty)
                {
                    queryable = queryable.Where(x => x.BuildingId == request.BuildingId);
                }
                if( request.FloorId != Guid.Empty)
                {
                    queryable = queryable.Where(x => x.FloorId == request.FloorId);
                }
                if ( request.RoomTypeId != Guid.Empty)
                {
                    queryable = queryable.Where(x => x.RoomTypeId == request.RoomTypeId);
                }
                if (!string.IsNullOrWhiteSpace(request.SearchString))
                {
                    queryable = queryable.Where(x => x.Name.ToLower().Trim().Contains(request.SearchString.ToLower().Trim()));
                }
                var result = await queryable.Where(x => x.RoomTypeStatus == EntityStatus.Active && x.FloorStatus == EntityStatus.Active && x.BuildingStatus == EntityStatus.Active).PaginateAsync(request, cancellationToken);
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

        public async Task<RequestResult<PaginationResponse<RoomDetailDto>>> GetRoomDetailWithPaginationByStatusAsync(ViewRoomDetailByCheckInCheckOutRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var queryable = _dbContext.RoomDetails.AsNoTracking().AsQueryable().Where(x => !x.Deleted).ProjectTo<RoomDetailDto>(_mapper.ConfigurationProvider)

                    .Where(x => request.CheckInBooking < x.CheckInBooking && request.CheckInBooking < x.CheckOutBooking
                    && request.CheckOutBooking < x.CheckInBooking && request.CheckOutBooking > x.CheckOutBooking
                    || x.Status == RoomStatus.Vacant);

        
                var result = await queryable.PaginateAsync(request, cancellationToken);
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
