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

        public async Task<BillDTO> GetBillById(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<BillDTO>($"/api/Bills/{id}");
            return result;
        }
        public async Task<BillDtoForService> GetBillByIdForService(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<BillDtoForService>($"/api/Bills/GetBillByIdForService{id}");
            return result;
        }

        public async Task<BillDtoForRoom> GetBillByIdForRoom(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<BillDtoForRoom>($"/api/Bills/GetBillByIdForRoom{id}");
            return result;
        }

        public async Task<PaginationResponse<BillDtoForRoom>> GetBillForRoom(ViewBillWithPaginationRequest request)
        {
            var url = $"/api/Bills/getBillsByOtherForRoom?PageNumber={request.PageNumber}&PageSize={request.PageSize}";

            if (!String.IsNullOrEmpty(request.Search))
            {
                url = $"/api/Bills/getBillsByOtherForRoom?SearchString={request.Search}&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            }
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<BillDtoForRoom>>(url);
            return result;
        }

        public async Task<PaginationResponse<BillDtoForService>> GetBillForService(ViewBillWithPaginationRequest request)
        {
            var url = $"/api/Bills/getBillsByOtherForService?PageNumber={request.PageNumber}&PageSize={request.PageSize}";


            if (!String.IsNullOrEmpty(request.Search))
            {
                url = $"/api/Bills/getBillsByOtherForService?SearchString={request.Search}&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            }
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<BillDtoForService>>(url);
            return result;
        }

        public async Task<List<BillDTO>> GetBillByIdCustomer(Guid idCustomer)
        {
            var result = await _httpClient.GetFromJsonAsync<List<BillDTO>>($"/api/Bills/{idCustomer}/details");
            return result;
        }


        public async Task<bool> UpdateBill(BillUpdateRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/Bills", request);
            return result.IsSuccessStatusCode;
        }
    }
}
