using AutoMapper;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
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
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Infrastructure.Extensions;
using BaseSolution.Application.DataTransferObjects.Services;
using AutoMapper.QueryableExtensions;
using BaseSolution.Application.DataTransferObjects.Services.Request;

namespace BaseSolution.Infrastructure.Implements.Repositories.ReadOnly
{
    public class ServiceReadOnlyRepository : IServiceReadOnlyRepository
    {
        private readonly DbSet<ServiceEntity> _ServiceEntities;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public ServiceReadOnlyRepository(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _ServiceEntities = dbContext.Set<ServiceEntity>();
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<List<ServiceDTO>>> GetServiceAsync(ViewServiceWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _ServiceEntities.AsNoTracking().Where(x => !x.Deleted).ProjectTo<ServiceDTO>(_mapper.ConfigurationProvider);

                var result = await query.PaginateAsync(request, cancellationToken);
              
                return RequestResult<List<ServiceDTO>>.Succeed( await query.ToListAsync());
            }
            catch (Exception e)
            {
                return RequestResult<List<ServiceDTO>>.Fail(_localizationService["List of service  are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of service "
                    }
                });
            }
        }

        public async Task<RequestResult<ServiceDTO?>> GetServiceByIdAsync(Guid idService, CancellationToken cancellationToken)
        {
            try
            {
                var Service = await _ServiceEntities.AsNoTracking().Where(c => c.Id == idService && !c.Deleted).ProjectTo<ServiceDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

                return RequestResult<ServiceDTO?>.Succeed(Service);
            }
            catch (Exception e)
            {
                return RequestResult<ServiceDTO?>.Fail(_localizationService["Service is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "Service"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<ServiceDTO>>> GetServiceWithPaginationByAdminAsync(ViewServiceWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<ServiceEntity> queryable = _ServiceEntities.AsNoTracking().AsQueryable().Where(x => !x.Deleted && x.ServiceTypeId == request.ServiceTypeId);
                if(!string.IsNullOrWhiteSpace(request.SearchString))
                {
                    queryable = queryable.Where(x => x.Name.ToLower().Trim().Contains(request.SearchString.ToLower().Trim()));
                }
                var result = await queryable.PaginateAsync<ServiceEntity, ServiceDTO>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<ServiceDTO>>.Succeed(new PaginationResponse<ServiceDTO>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<ServiceDTO>>.Fail(_localizationService["List of Service are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of Service"
                    }
                });
            }
        }
    }
}
