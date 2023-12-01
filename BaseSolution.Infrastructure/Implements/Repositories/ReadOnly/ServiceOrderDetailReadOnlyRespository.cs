using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseSolution.Application.DataTransferObjects.ServiceOrderDetail;
using BaseSolution.Application.DataTransferObjects.ServiceOrderDetail.Request;
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


namespace BaseSolution.Infrastructure.Implements.Repositories.ReadOnly
{
    public class ServiceOrderDetailReadOnlyRespository : IServiceOrderDetailReadOnlyRespository
    {
        private readonly AppReadOnlyDbContext _appReadOnlyDbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public ServiceOrderDetailReadOnlyRespository(AppReadOnlyDbContext appReadOnlyDbContext, IMapper mapper, ILocalizationService localizationService)
        {
            _appReadOnlyDbContext = appReadOnlyDbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<ServiceOrderDetailDTO?>> GetServiceOrderDetailByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var serviceOrderDetail = await _appReadOnlyDbContext.ServiceOrderDetails.AsNoTracking().Where(c => c.Id == id && !c.Deleted).ProjectTo<ServiceOrderDetailDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

                return RequestResult<ServiceOrderDetailDTO?>.Succeed(serviceOrderDetail);
            }
            catch (Exception e)
            {
                return RequestResult<ServiceOrderDetailDTO?>.Fail(_localizationService["Service order detail is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "service order detail"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<ServiceOrderDetailDTO>>> GetServiceOrderDetailsByAdminAsync(ViewServiceOrderDetailWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _appReadOnlyDbContext.ServiceOrderDetails.AsNoTracking()
                    .PaginateAsync<ServiceOrderDetailEntity, ServiceOrderDetailDTO>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<ServiceOrderDetailDTO>>.Succeed(new PaginationResponse<ServiceOrderDetailDTO>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<ServiceOrderDetailDTO>>.Fail(_localizationService["List of service order detail are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of service order detail"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<ServiceOrderDetailDTO>>> GetServiceOrderDetailsByOtherAsync(ViewServiceOrderDetailWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _appReadOnlyDbContext.ServiceOrderDetails.AsNoTracking().Where(x => !x.Deleted)
                    .PaginateAsync<ServiceOrderDetailEntity, ServiceOrderDetailDTO>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<ServiceOrderDetailDTO>>.Succeed(new PaginationResponse<ServiceOrderDetailDTO>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<ServiceOrderDetailDTO>>.Fail(_localizationService["List of service order detail are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of service order detail"
                    }
                });
            }
        }
        public async Task<RequestResult<List<ServiceOrderDetailDTO>>> GetServiceOrderDetailsByServiceOrderAsync(ViewServiceOrderDetailByIdServiceOderRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _appReadOnlyDbContext.ServiceOrderDetails.AsNoTracking().Where(x => x.Status != EntityStatus.Deleted).ProjectTo<ServiceOrderDetailDTO>(_mapper.ConfigurationProvider);

                if (request.idServiceOrder != Guid.Empty)
                {
                    query = query.Where(x => x.ServiceOrderId == request.idServiceOrder);
                }
                return RequestResult<List<ServiceOrderDetailDTO>>.Succeed( await query.ToListAsync(cancellationToken));
            }
            catch (Exception e)
            {
                return RequestResult<List<ServiceOrderDetailDTO>>.Fail(_localizationService["List of service order detail are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of service order detail"
                    }
                });
            }
        }
    }
}
