using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomDetail;
using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomDetail.Request;
using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomType;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using BaseSolution.BlazorServer.Respository.Interfaces;
using System.Web;
using System;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class RoomDetailRespo : IRoomDetailRespo
    {
        private readonly HttpClient _httpClient;
        public RoomDetailRespo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateNewRoomDetail(RoomDetailCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/RoomDetails", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteRoomDetail(RoomDetailDeleteRequest request)
        {
            string url = $"/api/RoomDetails?Id={request.Id}";
            if(request.DeletedBy != null || request.DeletedBy != Guid.Empty)
            {
                url += $"&DeletedBy={request.DeletedBy}";
            }
            var result = await _httpClient.DeleteAsync(url);
            return result.IsSuccessStatusCode;
        }

        public async Task<PaginationResponse<RoomDetailDTO>> GetAllRoomDetails(ViewRoomDetailWithPaginationRequest request)
        {
            string url = $"/api/RoomDetails";
            bool isSearch = false;
            if(request.BuildingId != null || request.BuildingId != Guid.Empty)
            {
                url += $"?BuildingId={request.BuildingId}";
                isSearch = true ;
            }
            if(request.FloorId != null || request.FloorId != Guid.Empty)
            {
                if(isSearch)
                {
                    url += $"&FloorId={request.FloorId}";
                }
                else
                {
                    url += $"?FloorId={request.FloorId}";
                    isSearch = true;
                }
            }
            if(request.RoomTypeId != null || request.RoomTypeId != Guid.Empty)
            {
                if (isSearch)
                {
                    url += $"&RoomTypeId={request.RoomTypeId}";
                }
                else
                {
                    url += $"?RoomTypeId={request.RoomTypeId}";
                    isSearch = true;
                }
            }
            if (!string.IsNullOrEmpty(request.SearchString))
            {
                if (isSearch)
                {
                    url += $"&SearchString={request.SearchString}";
                }
                else
                {
                    url += $"?SearchString={request.SearchString}";
                    isSearch = true;
                }
            }
            if (isSearch)
            {
                url += $"&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            }
            else
            {
                url += $"?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            }
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<RoomDetailDTO>>(url);
            return result;
        }


        public async Task<PaginationResponse<RoomDetailDTO>> GetAllRoomDetailsByStatus(ViewRoomDetailByCheckInCheckOutRequest request)
        {
            try
            {
                string url = $"/api/RoomDetails/getRoomBookingDetailByStatus?CheckInBooking={HttpUtility.UrlEncode(request.CheckInBooking.ToString("o"))}&CheckOutBooking={HttpUtility.UrlEncode(request.CheckOutBooking.ToString("o"))}";
                var result = await _httpClient.GetFromJsonAsync<PaginationResponse<RoomDetailDTO>>(url);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
         
           
        }
        public async Task<RoomDetailDTO> GetRoomDetailById(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<RoomDetailDTO>($"api/RoomDetails/{id}");
            return result;
        }

        public async Task<List<RoomDetailDTO>> GetRoomDetailByIdRoomType(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<List<RoomDetailDTO>>($"api/RoomDetails/idRoomType?idRoomType={id}");

            return result;
        }

        public async Task<bool> UpdateRoomDetail(RoomDetailUpdateRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/RoomDetails", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateRoomDetailStatus(RoomDetailUpdateStatusRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/RoomDetails/UpdateStatusRoomDetail", request);
            return result.IsSuccessStatusCode;
        }
    }
}
