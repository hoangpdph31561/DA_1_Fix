using BaseSolution.BlazorServer.Data.DataTransferObjects.Customer;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Customer.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface ICustomerRepo
    {
        Task<PaginationResponse<CustomerDTO>> GetCustomer(ViewCustomerWithPaginationRequest request);
        Task<bool> CreateCustomer(CustomerCreateRequest request);
        Task<bool> UpdateCustomer(CustomerUpdateRequest request);
        Task<CustomerDTO> GetIdentificationNumber(string identification);
        Task<bool> SignUpOrSignIn(CustomerCreateRequest customerCreateRequest);
        Task<bool> VerifyCode(string code, string idCard);  
        Task<CustomerDTO> GetCustomerById(Guid id);
    }
}
