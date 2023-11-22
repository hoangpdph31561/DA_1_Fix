using AutoMapper;
using BaseSolution.Application.DataTransferObjects.ServiceType.Request;
using BaseSolution.Application.DataTransferObjects.ServiceType;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using BaseSolution.Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Infrastructure.Extensions;

namespace BaseSolution.Infrastructure.Implements.Repositories.ReadOnly
{
    public class ServiceTypeReadOnlyRepository : IServiceTypeReadOnlyRepository
    {
        private readonly DbSet<ServiceTypeEntity> _ServiceTypeEntities;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public ServiceTypeReadOnlyRepository(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _ServiceTypeEntities = dbContext.Set<ServiceTypeEntity>();
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<ServiceTypeDto?>> GetServiceTypeByIdAsync(Guid idServiceType, CancellationToken cancellationToken)
        {
            try
            {
                var ServiceType = await _ServiceTypeEntities.AsNoTracking().Where(c => c.Id == idServiceType && !c.Deleted).ProjectTo<ServiceTypeDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

                return RequestResult<ServiceTypeDto?>.Succeed(ServiceType);
            }
            catch (Exception e)
            {
                return RequestResult<ServiceTypeDto?>.Fail(_localizationService["ServiceType is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "ServiceType"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<ServiceTypeDto>>> GetServiceTypeWithPaginationByAdminAsync(ViewServiceTypeWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<ServiceTypeEntity> queryable = _ServiceTypeEntities.AsNoTracking().AsQueryable().Where(x => !x.Deleted);
                if(!string.IsNullOrWhiteSpace(request.SearchString))
                {
                    queryable = queryable.Where(x => x.Name.ToLower().Trim().Contains(request.SearchString.ToLower().Trim()));
                }
                var result = await queryable
                    .PaginateAsync<ServiceTypeEntity, ServiceTypeDto>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<ServiceTypeDto>>.Succeed(new PaginationResponse<ServiceTypeDto>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<ServiceTypeDto>>.Fail(_localizationService["List of ServiceType are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of ServiceType"
                    }
                });
            }
        }
    }
}