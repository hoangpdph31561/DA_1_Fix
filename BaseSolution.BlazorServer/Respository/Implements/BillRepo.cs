using BaseSolution.BlazorServer.Data.DataTransferObjects.Bill;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Bill.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using BaseSolution.BlazorServer.Respository.Interfaces;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class BillRepo : IBillRepo
    {
        private readonly HttpClient _httpClient;
        public BillRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginationResponse<BillDTO>> GetAllBill(ViewCustomerBillWithPaginationRequest request)
        {
            string url = $"/api/Bills/getBillByOther?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<BillDTO>>(url);
            return result;
        }
    }
}
