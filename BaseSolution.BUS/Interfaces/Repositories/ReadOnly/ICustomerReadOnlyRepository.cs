using BaseSolution.Application.DataTransferObjects.Customer.Request;
using BaseSolution.Application.DataTransferObjects.Customer;
using BaseSolution.Application.ValueObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseSolution.Application.ValueObjects.Pagination;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface ICustomerReadOnlyRepository
    {
        Task<RequestResult<CustomerDto?>> GetCustomerByIdAsync(Guid idCustomer, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<CustomerDto>>> GetCustomerWithPaginationByAdminAsync(
            ViewCustomerWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<CustomerDto?>> GetCustomerByIdentificationAsync(string identification, CancellationToken cancellationToken);
        Task<RequestResult<CustomerDto?>> GetCustomerByEmailAsync(string Email, CancellationToken cancellationToken);
    }
}
