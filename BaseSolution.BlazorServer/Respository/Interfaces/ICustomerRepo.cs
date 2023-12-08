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
        Task<bool> UpdateDetailCustomer(CustomerDetailUpdateRequest request);
        Task<bool> DeleteCustomer(CustomerDeleteRequest request);
        Task<CustomerDTO> GetIdentificationNumber(string identification);
        Task<CustomerDTO> GetEmailAsync(string email);
        Task<bool> SignUpOrSignIn(CustomerCreateRequest customerCreateRequest);
        Task<bool> VerifyCode(string code, string idCard);  
        Task<CustomerDTO> GetCustomerById(Guid id);
        Task<bool> UpdateStatusCustomer(Guid id);
        Task<bool> VerifyCustomerBooking(Guid id, string Identification, string code);
    }
}
