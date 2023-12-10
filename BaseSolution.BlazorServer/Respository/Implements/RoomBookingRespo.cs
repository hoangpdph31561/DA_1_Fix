using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBooking;
using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBooking.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using BaseSolution.BlazorServer.Respository.Interfaces;
using System.Net.Http;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class RoomBookingRespo : IRoomBookingRespo
    {
        private readonly HttpClient _httpClient;

        public RoomBookingRespo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateNewRoomBooking(RoombookingCreateRequest request)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync("/api/Roombookings", request);
                return result.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<Guid> CreateRoomBooking(RoombookingCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Roombookings/postByCustomer", request);

            if (result.IsSuccessStatusCode)
            {
                var convert = await result.Content.ReadFromJsonAsync<RoomBookingRespone>();
                return convert.Data;
            }
            else
            {
                throw new Exception("Lỗi trong quá trình tạo mới phòng");
            }
        }

        public async Task<bool> DeleteRoomBooking(RoomBookingDeleteRequest request)
        {
            string url = $"/api/Roombookings?Id={request.Id}";
            if (request.DeletedBy != null)
            {
                url += $"&DeletedBy={request.DeletedBy}";
            }
            var result = await _httpClient.DeleteAsync(url);
            return result.IsSuccessStatusCode;
        }

        public async Task<PaginationResponse<RoomBookingDto>> GetAllRoomBooking(ViewRoombookingPaginationRequest request)
        {
            try
            {
                string url = $"/api/RoomBookings/getRoomBookingByOther?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
                if (!String.IsNullOrEmpty(request.SearchString))
                {
                    url = $"/api/RoomBookings/getRoomBookingByOther?SearchString={request.SearchString}&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
                }
                var result = await _httpClient.GetFromJsonAsync<PaginationResponse<RoomBookingDto>>(url);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PaginationResponse<RoomBookingDto>> GetAllRoomBookingByAwait(ViewRoombookingPaginationRequest request)
        {
            try
            {
                string url = $"/api/RoomBookings/getRoomBookingByAwait?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
                if (!String.IsNullOrEmpty(request.SearchString))
                {
                    url = $"/api/RoomBookings/getRoomBookingByAwait?SearchString={request.SearchString}&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
                }
                var result = await _httpClient.GetFromJsonAsync<PaginationResponse<RoomBookingDto>>(url);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RoomBookingDto> GetRoomBookingById(Guid id)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<RoomBookingDto>($"/api/Roombookings/{id}");
                return result;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public async Task<List<RoomBookingDto>> GetRoomBookingByIdCustomerAsync(Guid idCustomer)
        {
            var result = await _httpClient.GetFromJsonAsync<List<RoomBookingDto>>($"/api/Roombookings/{idCustomer}/details");
            return result;
        }

        public async Task<bool> UpdateRoomBooking(RoombookingUpdateRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/Roombookings", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateStatusRoomBooking(RoomBookingUpdateStatusRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/Roombookings/updateStatus", request);
            return result.IsSuccessStatusCode;
        }

    }
}
