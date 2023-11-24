using BaseSolution.BlazorServer.Data.DataTransferObjects.Bill;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Bill.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using BaseSolution.BlazorServer.Respository.Interfaces;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class BillRespo : IBillRespo
    {
        private readonly HttpClient _httpClient;

        public BillRespo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateNewBill(BillCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Bills", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<PaginationResponse<BillDTO>> GetAllBill(ViewBillWithPaginationRequest request)
        {
            try
            {
                string url = $"/api/Bills/getBillByOther?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
                var result = await _httpClient.GetFromJsonAsync<PaginationResponse<BillDTO>>(url);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
