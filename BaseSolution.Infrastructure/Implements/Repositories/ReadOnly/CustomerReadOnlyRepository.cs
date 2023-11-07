using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Customer;
using BaseSolution.Application.DataTransferObjects.Customer.Request;
using BaseSolution.Application.DataTransferObjects.Customer;
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
using AutoMapper.QueryableExtensions;

namespace BaseSolution.Infrastructure.Implements.Repositories.ReadOnly
{
    public class CustomerReadOnlyRepository : ICustomerReadOnlyRepository
    {

        private readonly DbSet<CustomerEntity> _CustomerEntities;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public CustomerReadOnlyRepository(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _CustomerEntities = dbContext.Set<CustomerEntity>();
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<CustomerDto?>> GetCustomerByIdAsync(Guid idCustomer, CancellationToken cancellationToken)
        {

            try
            {
                var Customer = await _CustomerEntities.AsNoTracking().Where(c => c.Id == idCustomer && !c.Deleted).ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

                return RequestResult<CustomerDto?>.Succeed(Customer);
            }
            catch (Exception e)
            {
                return RequestResult<CustomerDto?>.Fail(_localizationService["Customer is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "Customer"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<CustomerDto>>> GetCustomerWithPaginationByAdminAsync(ViewCustomerWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<CustomerEntity> queryable = _CustomerEntities.AsNoTracking().AsQueryable();
                var result = await _CustomerEntities.AsNoTracking()
                    .PaginateAsync<CustomerEntity, CustomerDto>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<CustomerDto>>.Succeed(new PaginationResponse<CustomerDto>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<CustomerDto>>.Fail(_localizationService["List of Customer are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of Customer"
                    }
                });
            }
        }
    }
}
