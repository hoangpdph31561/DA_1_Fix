using BaseSolution.BlazorServer.Data.DataTransferObjects.Customer;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Customer.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface ICustomerRepo
    {
        Task<PaginationResponse<CustomerDTO>> GetCustomer(ViewCustomerWithPaginationRequest request);
        Task<bool> CreateCustomer(CustomerCreateRequest request);
        Task<CustomerDTO> GetIdentificationNumber(string identification);
    }
}
