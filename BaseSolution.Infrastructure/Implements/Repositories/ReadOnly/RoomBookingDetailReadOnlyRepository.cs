using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseSolution.Application.DataTransferObjects.RoomBookingDetail;
using BaseSolution.Application.DataTransferObjects.RoomBookingDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Enums;
using BaseSolution.Infrastructure.Database.AppDbContext;
using BaseSolution.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BaseSolution.Infrastructure.Implements.Repositories.ReadOnly
{
    public class RoomBookingDetailReadOnlyRepository : IRoomBookingDetailReadOnlyRepository
    {
        private readonly AppReadOnlyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public RoomBookingDetailReadOnlyRepository(AppReadOnlyDbContext appReadOnlyDbContext, IMapper mapper, ILocalizationService localizationService)
        {
            _dbContext = appReadOnlyDbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<RoomBookingDetailDTO?>> GetRoomBookingDetailByIdAsync(Guid idRoomBookingDetail, CancellationToken cancellationToken)
        {
            try
            {
                var roombooking = await _dbContext.RoomBookingDetails.AsNoTracking().Where(x => x.Id == idRoomBookingDetail).ProjectTo<RoomBookingDetailDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
                return RequestResult<RoomBookingDetailDTO?>.Succeed(roombooking);

            }
            catch (Exception e)
            {

                return RequestResult<RoomBookingDetailDTO?>.Fail(_localizationService["RoomBookingDetail is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "roomBookingDetail"
                    }
                });
            }
        }

        public async Task<RequestResult<RoomBookingDetailDTO?>> GetRoomBookingDetailByIdRoomBookingAsync(Guid idRoomBooking, CancellationToken cancellationToken)
        {
            try
            {
                var roombooking = await _dbContext.RoomBookingDetails.AsNoTracking().Where(x => x.RoomBookingId == idRoomBooking).ProjectTo<RoomBookingDetailDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
                return RequestResult<RoomBookingDetailDTO?>.Succeed(roombooking);

            }
            catch (Exception e)
            {

                return RequestResult<RoomBookingDetailDTO?>.Fail(_localizationService["RoomBookingDetail is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "roomBookingDetail"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<RoomBookingDetailDTO>>> GetRoomBookingDetailWithPaginationByAdminAsync(ViewRoomBookingDetailWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query =  _dbContext.RoomBookingDetails.AsNoTracking().ProjectTo<RoomBookingDetailDTO>(_mapper.ConfigurationProvider);
                
               var result = await query.PaginateAsync(request, cancellationToken);
                return RequestResult<PaginationResponse<RoomBookingDetailDTO>>.Succeed(new PaginationResponse<RoomBookingDetailDTO>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data!
                });
            }
            catch (Exception e)
            {

                return RequestResult<PaginationResponse<RoomBookingDetailDTO>>.Fail(_localizationService["List of RoomBookingDetail are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error= e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of RoomBookingDetail"
                    }
                });
            }
        }
        public async Task<RequestResult<PaginationResponse<RoomBookingDetailDTO>>> GetRoomBookingDetailWithPaginationByOtherAsync(ViewRoomBookingDetailWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _dbContext.RoomBookingDetails.AsNoTracking().Where(x => x.Status != EntityStatus.Deleted).ProjectTo<RoomBookingDetailDTO>(_mapper.ConfigurationProvider);
                var result = await query.PaginateAsync(request, cancellationToken);
                return RequestResult<PaginationResponse<RoomBookingDetailDTO>>.Succeed(new PaginationResponse<RoomBookingDetailDTO>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data!
                });
            }
            catch (Exception e)
            {

                return RequestResult<PaginationResponse<RoomBookingDetailDTO>>.Fail(_localizationService["List of RoomBookingDetail are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error= e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of RoomBookingDetail"
                    }
                });
            }
        }

        public async Task<RequestResult<RoomBookingDetailDTO>> GetRoomBookingDetailWithPaginationByIdRoomBookingAsync(Guid idRoomBooking, CancellationToken cancellationToken)
        {
            try
            {
                var query = await _dbContext.RoomBookingDetails.AsNoTracking().Where(x => x.Status != EntityStatus.Deleted && idRoomBooking == x.RoomBookingId)
                    .ProjectTo<RoomBookingDetailDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);

                return RequestResult<RoomBookingDetailDTO>.Succeed(query);
            }
            catch (Exception e)
            {

                return RequestResult<RoomBookingDetailDTO>.Fail(_localizationService["List of RoomBookingDetail are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error= e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of RoomBookingDetail"
                    }
                });
            }
        }
    }
}
