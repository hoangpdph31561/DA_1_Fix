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

                foreach (var item in result.Data!)
                {
                    item.TotalAmount = (float)item.Price * item.Quantity;
                }
                return RequestResult<PaginationResponse<ServiceOrderDTO>>.Succeed(new PaginationResponse<ServiceOrderDTO>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
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

                foreach (var item in result.Data!)
                {
                    item.TotalAmount = (float)item.Price * item.Quantity;
                }
                return RequestResult<PaginationResponse<ServiceOrderDTO>>.Succeed(new PaginationResponse<ServiceOrderDTO>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
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
