using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Customer;
using BaseSolution.Application.DataTransferObjects.Customer.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using BaseSolution.Infrastructure.Database.AppDbContext;
using BaseSolution.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using BaseSolution.Domain.Enums;

namespace BaseSolution.Infrastructure.Implements.Repositories.ReadOnly
{
    public class CustomerReadOnlyRepository : ICustomerReadOnlyRepository
    {

        private readonly AppReadOnlyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public CustomerReadOnlyRepository(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<CustomerDto?>> GetCustomerByEmailAsync(string Email, CancellationToken cancellationToken)
        {
            try
            {
                var Customer = await _dbContext.Customers.AsNoTracking().Where(c => c.Email == Email && !c.Deleted).ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
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

        public async Task<RequestResult<CustomerDto?>> GetCustomerByIdAsync(Guid idCustomer, CancellationToken cancellationToken)
        {

            try
            {
                var Customer = await _dbContext.Customers.AsNoTracking().Where(c => c.Id == idCustomer && !c.Deleted).ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
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

        public async Task<RequestResult<CustomerDto>> GetCustomerByIdentificationAsync(string identification, CancellationToken cancellationToken)
        {
            try
            {
                var Customer = await _dbContext.Customers.AsNoTracking().Where(c => c.IdentificationNumber == identification && !c.Deleted).ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

                return RequestResult<CustomerDto>.Succeed(Customer);
            }
            catch (Exception e)
            {
                return RequestResult<CustomerDto>.Fail(_localizationService["Customer is not found"], new[]
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
                IQueryable<CustomerEntity> queryable = _dbContext.Customers.AsNoTracking().AsQueryable().Where(x => x.Status != EntityStatus.Deleted);
                if (!String.IsNullOrWhiteSpace(request.Name))
                {
                    queryable = queryable.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));
                }
                var result = await queryable.AsNoTracking()
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
