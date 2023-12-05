using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseSolution.Application.DataTransferObjects.ServiceOrder;
using BaseSolution.Application.DataTransferObjects.ServiceOrder.Request;
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
    public class ServiceOrderReadOnlyRespository : IServiceOrderReadOnlyRespository
    {
        private readonly AppReadOnlyDbContext _appReadOnlyDbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public ServiceOrderReadOnlyRespository(AppReadOnlyDbContext appReadOnlyDbContext, IMapper mapper, ILocalizationService localizationService)
        {
            _appReadOnlyDbContext = appReadOnlyDbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<List<ServiceOrderForRoomBookingDTO>>> GetServiceOrderByIdRoomBookingAsync(Guid idRoombooking, CancellationToken cancellationToken)
        {
            try
            {
                var getList = _appReadOnlyDbContext.ServiceOrders.AsNoTracking().ProjectTo<ServiceOrderForRoomBookingDTO>(_mapper.ConfigurationProvider);
                var listByType = await getList.Where(c => c.RoomBookingDetailId == idRoombooking).ToListAsync();
                List<ServiceOrderForRoomBookingDTO> lstTepRests = null;
                lstTepRests = listByType.GroupBy(c => new
                {
                    c.ServiceName,
                    c.ServiceId,
                    c.RoomBookingDetailId,
                    c.CustomerId,
                }).Select(grb => new ServiceOrderForRoomBookingDTO()
                {
                    ServiceName = grb.Key.ServiceName,
                    ServiceId = grb.Key.ServiceId,
                    Quantity = grb.Count(),
                    RoomBookingDetailId = grb.Key.RoomBookingDetailId,
                    CustomerId = grb.Key.CustomerId
                }).ToList();
                return RequestResult<List<ServiceOrderForRoomBookingDTO>>.Succeed(lstTepRests);
            }
            catch (Exception e)
            {
                return RequestResult<List<ServiceOrderForRoomBookingDTO>>.Fail(_localizationService["ServiceOrder is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "ServiceOrder"
                    }
                });
            }
        }

        public async Task<RequestResult<ServiceOrderDTO?>> GetServiceOrderByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var serviceOrder = await _appReadOnlyDbContext.ServiceOrders.AsNoTracking().Where(c => c.Id == id && !c.Deleted).ProjectTo<ServiceOrderDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);


                return RequestResult<ServiceOrderDTO?>.Succeed(serviceOrder);
            }
            catch (Exception e)
            {
                return RequestResult<ServiceOrderDTO?>.Fail(_localizationService["Service Order is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "Service order"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<ServiceOrderDTO>>> GetServicesByAdminAsync(ViewServiceOrderWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _appReadOnlyDbContext.ServiceOrders.AsNoTracking().ProjectTo<ServiceOrderDTO>(_mapper.ConfigurationProvider);

                if (!string.IsNullOrWhiteSpace(request.SearchString))
                {
                    query = query.Where(x => x.CustomerName.Contains(request.SearchString!));
                }
                var result = await query.PaginateAsync(request, cancellationToken);
                List<ServiceOrderDTO> lstTepRests = null;
                lstTepRests = result.Data!.GroupBy(c => new
                {
                    c.Price,
                    c.ServiceName,
                    c.CustomerName,
                    c.CustomerId,
                    c.ServiceId,
                    c.Status,
                    c.RoomBookingDetailId,
                }).Select(grb => new ServiceOrderDTO()
                {
                    Price = grb.Key.Price,
                    ServiceName = grb.Key.ServiceName,
                    CustomerName = grb.Key.CustomerName,
                    CustomerId = grb.Key.CustomerId,
                    ServiceId = grb.Key.ServiceId,
                    Quantity = grb.Count(),
                    TotalAmount = grb.Key.Price * grb.Count(),
                    Status = grb.Key.Status,
                    Id = result.Data!.Where(x => x.CustomerId == grb.Key.CustomerId).Select(x => x.Id).FirstOrDefault(),
                    CreatedTime = result.Data!.Where(x => x.CustomerId == grb.Key.CustomerId).Select(x => x.CreatedTime).FirstOrDefault(),
                    RoomBookingDetailId = grb.Key.RoomBookingDetailId
                }).ToList();
                return RequestResult<PaginationResponse<ServiceOrderDTO>>.Succeed(new PaginationResponse<ServiceOrderDTO>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = lstTepRests
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<ServiceOrderDTO>>.Fail(_localizationService["List of service orders are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of service order"
                    }
                });
            }
        }
        public async Task<RequestResult<PaginationResponse<ServiceOrderDTO>>> GetServicesByOtherAsync(ViewServiceOrderWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _appReadOnlyDbContext.ServiceOrders.AsNoTracking().ProjectTo<ServiceOrderDTO>(_mapper.ConfigurationProvider).Where(x => x.Status != EntityStatus.Deleted);

                if (!string.IsNullOrWhiteSpace(request.SearchString))
                {
                    query = query.Where(x => x.CustomerName.Contains(request.SearchString!));
                }
                    var result = await query.PaginateAsync(request, cancellationToken);

                List<ServiceOrderDTO> lstTepRests = null;
                lstTepRests = result.Data!.GroupBy(c => new
                {
                    c.Price,
                    c.ServiceName,
                    c.CustomerName,
                    c.CustomerId,
                    c.ServiceId,
                    c.Status,
                    c.RoomBookingDetailId
                }).Select(grb => new ServiceOrderDTO()
                {
                    Price = grb.Key.Price,
                    ServiceName = grb.Key.ServiceName,
                    CustomerName = grb.Key.CustomerName,
                    CustomerId = grb.Key.CustomerId,
                    ServiceId = grb.Key.ServiceId,
                    Quantity = grb.Count(),
                    TotalAmount = grb.Key.Price * grb.Count(),
                    Status = grb.Key.Status,
                    RoomBookingDetailId = grb.Key.RoomBookingDetailId,
                    Id = result.Data!.Where(x => x.CustomerId == grb.Key.CustomerId).Select(x => x.Id).FirstOrDefault(),
                    CreatedTime = result.Data!.Where(x => x.CustomerId == grb.Key.CustomerId).Select(x => x.CreatedTime).FirstOrDefault(),

                }).ToList();
                return RequestResult<PaginationResponse<ServiceOrderDTO>>.Succeed(new PaginationResponse<ServiceOrderDTO>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = lstTepRests
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<ServiceOrderDTO>>.Fail(_localizationService["List of service orders are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of service order"
                    }
                });
            }
        }
    }
}
