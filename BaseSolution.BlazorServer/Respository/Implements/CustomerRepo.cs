using BaseSolution.BlazorServer.Data.DataTransferObjects.Customer;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Customer.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using BaseSolution.BlazorServer.Respository.Interfaces;
using System.Text.Json;
using System.Text;
using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBooking.Request;

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
            if (!String.IsNullOrEmpty(request.Name))
            {
                url = $"/api/Customers?Name={request.Name}&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            }
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<CustomerDTO>>(url);
            return result;
        }
        public async Task<bool> SignUpOrSignIn(CustomerCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Customers/SignUpOrSignIn", request);
            if (result.IsSuccessStatusCode)
            {
                var convert = result.Content.ReadAsStringAsync();
                return result.IsSuccessStatusCode;
            }
            else
            {
                throw new Exception("Kiểm tra thông tin đăng nhập!(Mã định danh, Email phải của cá nhân)");
            }
        }
        public async Task<bool> VerifyCode(string code, string idCard)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/Customers/verifyCode/{code}/{idCard}", "");
            var convert = result.Content.ReadAsStringAsync();
            return result.IsSuccessStatusCode;
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

        public async Task<CustomerDTO> GetCustomerById(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<CustomerDTO>($"/api/Customers/{id}");
            return result;
        }

        public async Task<bool> UpdateCustomer(CustomerUpdateRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/Customers", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCustomer(CustomerDeleteRequest request)
        {
            string url = $"api/Customers?Id={request.Id}";
            if (request.DeletedBy != null)
            {
                url += $"&DeletedBy={request.DeletedBy}";
            }
            var result = await _httpClient.DeleteAsync(url);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateStatusCustomer(Guid id)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/Customers/UpdateStatusCustomer/{id}", string.Empty);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> VerifyCustomerBooking(Guid id, string Identification, string code)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/Customers/verifyCustomerBooking/{id}/{Identification}/{code}", "");
            var convert = result.Content.ReadAsStringAsync();
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateDetailCustomer(CustomerDetailUpdateRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/Customers/putByCustomer", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<CustomerDTO> GetEmailAsync(string email)
        {
            var existingEmail = await _httpClient.GetFromJsonAsync<CustomerDTO>($"/api/Customers/checkEmailExist?email={email}");

            return existingEmail;
        }
    }
}
