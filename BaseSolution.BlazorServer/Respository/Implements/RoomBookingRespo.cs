﻿using BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBooking;
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
            var result = await _httpClient.PostAsJsonAsync("/api/Roombookings", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<PaginationResponse<RoomBookingDto>> GetAllRoomBooking(ViewRoombookingPaginationRequest request)
        {
            try
            {
                string url = $"/api/Roombookings/getRoomBookingByOther?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
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
            var result = await _httpClient.GetFromJsonAsync<RoomBookingDto>($"/api/Roombookings/{id}");
            return result;
        }

        public async Task<bool> UpdateRoomBooking(RoombookingUpdateRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/Roombookings", request);
            return result.IsSuccessStatusCode;
        }
    }
}