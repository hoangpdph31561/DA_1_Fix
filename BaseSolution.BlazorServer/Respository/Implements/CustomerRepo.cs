using BaseSolution.BlazorServer.Data.DataTransferObjects.Customer;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Customer.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using BaseSolution.BlazorServer.Respository.Interfaces;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly HttpClient _httpClient;
        public CustomerRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<PaginationResponse<CustomerDTO>> GetCustomer(ViewCustomerWithPaginationRequest request)
        {
            string url = $"/api/Customers?PageNumber={request.PageNumber}&PageSize={request.PageSize}";

            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<CustomerDTO>>(url);
            return result;
        }
        public async Task<bool> CreateCustomer(CustomerCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Customers", request);

            return result.IsSuccessStatusCode;
        }
        public async Task<CustomerDTO> GetIdentificationNumber(string identificationNumber)
        {
            var existingCustomer = await _httpClient.GetFromJsonAsync<CustomerDTO>($"/api/Customers/{identificationNumber}/details");

            return existingCustomer;
        }
    }
}
