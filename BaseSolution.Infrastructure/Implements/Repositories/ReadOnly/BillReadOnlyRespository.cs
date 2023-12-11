using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseSolution.Application.DataTransferObjects.Bill;
using BaseSolution.Application.DataTransferObjects.Bill.Request;
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
    public class BillReadOnlyRespository : IBillReadOnlyRespository
    {
        private readonly AppReadOnlyDbContext _appReadOnlyDbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public BillReadOnlyRespository(AppReadOnlyDbContext appReadOnlyDbContext, IMapper mapper, ILocalizationService localizationService)
        {
            _appReadOnlyDbContext = appReadOnlyDbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<BillDTO?>> GetBillByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var bill = await _appReadOnlyDbContext.Bills.AsNoTracking().Where(c => c.Id == id && !c.Deleted).ProjectTo<BillDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
                var userCreated = await _appReadOnlyDbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == bill!.CreatedBy, cancellationToken) == null ? "N/A" : _appReadOnlyDbContext.Users.AsNoTracking().First(x => x.Id == bill!.CreatedBy)!.Name;
                bill!.CreatedUserName = userCreated;
                return RequestResult<BillDTO?>.Succeed(bill);
            }
            catch (Exception e)
            {
                return RequestResult<BillDTO?>.Fail(_localizationService["Bill is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "Bill"
                    }
                });
            }
        }


        public async Task<RequestResult<BillDtoForRoom?>> GetBillByIdForRoomAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var bill = await _appReadOnlyDbContext.Bills.AsNoTracking().Where(c => c.Id == id && !c.Deleted).ProjectTo<BillDtoForRoom>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

                var ServiceOrderDetail = _appReadOnlyDbContext.ServiceOrderDetails.Where(x => x.ServiceOrderId == bill.ServiceOrderId && !x.Deleted).ToList();
                bill.ServiceAmount = 0;
                foreach (var service in ServiceOrderDetail)
                {
                    bill.ServiceAmount += service.Price * (decimal)service.Amount;
                }
                bill.RoomAmount = UtilityExtensions.TinhTien(bill.CheckInReality, bill.CheckOutReality, bill.RoomPrice, bill.PrePaid);
                bill.TotalAmount = bill.ServiceAmount + bill.RoomAmount;
                return RequestResult<BillDtoForRoom?>.Succeed(bill);
            }
            catch (Exception e)
            {
                return RequestResult<BillDtoForRoom?>.Fail(_localizationService["Bill is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "Bill"
                    }
                });
            }
        }

        public async Task<RequestResult<List<BillDTO>>> GetBillByIdCustomerAsync(Guid idCustomer, CancellationToken cancellationToken)
        {
            try
            {
                var bills = await _appReadOnlyDbContext.Bills.AsNoTracking()
                    .Where(c => c.CustomerId == idCustomer && !c.Deleted)
                    .ProjectTo<BillDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);


                return RequestResult<List<BillDTO>>.Succeed(bills);

            }
            catch (Exception e)
            {
                return RequestResult<List<BillDTO>>.Fail(_localizationService["Bill is not found"], new[]

                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "Bill"
                    }
                });
            }
        }


        public async Task<RequestResult<BillDtoForService?>> GetBillByIdForServiceAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var bill = await _appReadOnlyDbContext.Bills.AsNoTracking().Where(c => c.Id == id && !c.Deleted).ProjectTo<BillDtoForService>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
                var serviceOrderDetail = _appReadOnlyDbContext.ServiceOrderDetails.Where(x => x.ServiceOrderId == bill.ServiceOrderId && !x.Deleted).ToList();
                bill.TotalAmount = 0;
                foreach (var services in serviceOrderDetail)
                {
                    bill.TotalAmount += services.Price * (decimal)services.Amount;
                }
                return RequestResult<BillDtoForService?>.Succeed(bill);
            }
            catch (Exception e)
            {
                return RequestResult<BillDtoForService?>.Fail(_localizationService["Bill is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "Bill"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<BillDTO>>> GetBillsByAdminAsync(ViewBillWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _appReadOnlyDbContext.Bills.AsNoTracking().ProjectTo<BillDTO>(_mapper.ConfigurationProvider);

                if (!string.IsNullOrWhiteSpace(request.SearchString))
                {
                    query = query.Where(x => x.CustomerName == request.SearchString);
                }
                var result = await query.PaginateAsync(request, cancellationToken);

                foreach (var item in result.Data!)
                {
                    var userCreated = await _appReadOnlyDbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.CreatedBy, cancellationToken) == null ? "N/A" : _appReadOnlyDbContext.Users.AsNoTracking().First(x => x.Id == item.CreatedBy)!.Name;
                    item.CreatedUserName = userCreated;

                    item.ServiceAmount = (float)(item.TotalService * item.ServicePrice);

                    // tính  tổng tiền 
                    item.TotalAmount = item.ServiceAmount + (float)item.RoomPrice;
                }
                return RequestResult<PaginationResponse<BillDTO>>.Succeed(new PaginationResponse<BillDTO>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<BillDTO>>.Fail(_localizationService["List of Bill are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of bill"
                    }
                });
            }
        }
        public async Task<RequestResult<PaginationResponse<BillDTO>>> GetBillsByOtherAsync(ViewBillWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _appReadOnlyDbContext.Bills.AsNoTracking().Where(x => !x.Deleted).ProjectTo<BillDTO>(_mapper.ConfigurationProvider);


                if (!string.IsNullOrWhiteSpace(request.SearchString))
                {
                    query = query.Where(x => x.CustomerName == request.SearchString);
                }
                var result = await query.PaginateAsync(request, cancellationToken);

                foreach (var item in result.Data!)
                {
                    var userCreated = await _appReadOnlyDbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.CreatedBy, cancellationToken) == null ? "N/A" : _appReadOnlyDbContext.Users.AsNoTracking().First(x => x.Id == item.CreatedBy)!.Name;
                    item.CreatedUserName = userCreated;
                    item.ServiceAmount = (float)(item.TotalService * item.ServicePrice);
                    var roomType = await _appReadOnlyDbContext.RoomBookings.AsNoTracking().FirstOrDefaultAsync(x => x.CustomerId == item.CustomerId, cancellationToken) == null ? "Bill Dịch vụ" : "Bill phòng";
                    item.BillType = roomType;
                    // tính  tổng tiền 
                    item.TotalAmount = item.ServiceAmount + (float)item.RoomPrice;

                }
                return RequestResult<PaginationResponse<BillDTO>>.Succeed(new PaginationResponse<BillDTO>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<BillDTO>>.Fail(_localizationService["List of Bill are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of bill"
                    }
                });
            }
        }
        public async Task<RequestResult<PaginationResponse<BillDtoForRoom>>> GetBillsByOtherForRoom(ViewBillWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _appReadOnlyDbContext.Bills.AsNoTracking().Where(x => !x.Deleted).ProjectTo<BillDtoForRoom>(_mapper.ConfigurationProvider);


                if (!string.IsNullOrWhiteSpace(request.SearchString))
                {
                    query = query.Where(x => x.CustomerName.Contains(request.SearchString));
                }
                var result = await query.Where(x => x.StatusRoomBooking == EntityStatus.InActive).PaginateAsync(request, cancellationToken);

                foreach (var item in result.Data!)
                {
                    var ServiceOrderDetail = _appReadOnlyDbContext.ServiceOrderDetails.Where(x => x.ServiceOrderId == item.ServiceOrderId &&!x.Deleted).ToList();
                    item.ServiceAmount = 0;
                    foreach (var service in ServiceOrderDetail)
                    {
                        item.ServiceAmount += service.Price * (decimal)service.Amount;
                    }
                    item.RoomAmount = UtilityExtensions.TinhTien(item.CheckInReality, item.CheckOutReality, item.RoomPrice, item.PrePaid);
                    item.TotalAmount = item.ServiceAmount + item.RoomAmount;
                }
                return RequestResult<PaginationResponse<BillDtoForRoom>>.Succeed(new PaginationResponse<BillDtoForRoom>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<BillDtoForRoom>>.Fail(_localizationService["List of billForRoom are not found"], new[]
               {
                    new ErrorItem
                    {
                        Error= e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of billForRoom"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<BillDtoForService>>> GetBillsByOtherForService(ViewBillWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _appReadOnlyDbContext.Bills.AsNoTracking().Where(x => !x.Deleted).ProjectTo<BillDtoForService>(_mapper.ConfigurationProvider);

                if (!string.IsNullOrWhiteSpace(request.SearchString))
                {
                    query = query.Where(x => x.CustomerName.Contains(request.SearchString));
                }
                var result = await query.Where(x => x.StatusServicerOrder == EntityStatus.InActive && x.RoomBookingDetailId == null).PaginateAsync(request, cancellationToken);
                foreach (var item in result.Data!)
                {
                    var serviceOrderDetail = _appReadOnlyDbContext.ServiceOrderDetails.Where(x => x.ServiceOrderId == item.ServiceOrderId && !x.Deleted).ToList();
                    item.TotalAmount = 0;
                    foreach (var services in serviceOrderDetail)
                    {
                        item.TotalAmount += services.Price * (decimal)services.Amount;
                    }
                }
                return RequestResult<PaginationResponse<BillDtoForService>>.Succeed(new PaginationResponse<BillDtoForService>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<BillDtoForService>>.Fail(_localizationService["List of billService are not found"], new[]
               {
                    new ErrorItem
                    {
                        Error= e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of billService"
                    }
                });
            }
        }
    }
}
