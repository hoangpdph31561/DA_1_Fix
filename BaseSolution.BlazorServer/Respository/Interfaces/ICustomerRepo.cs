using BaseSolution.BlazorServer.Data.DataTransferObjects.Customer;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Customer.Request;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface ICustomerRepo
    {
        Task<bool> CreateCustomer(CustomerCreateRequest request);
        Task<CustomerDTO> GetIdentificationNumber(string identification);
    }
}
