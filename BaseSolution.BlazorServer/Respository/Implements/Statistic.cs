using BaseSolution.BlazorServer.Data.DataTransferObjects.Statistic;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Statistic.Request;
using BaseSolution.BlazorServer.Respository.Interfaces;

namespace BaseSolution.BlazorServer.Respository.Implements
{
    public class Statistic : IStatistic
    {
        private readonly HttpClient _httpClient;
        public Statistic(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<BillStatisticDto>> GetBillStatisticsAsync(BillStatisticRequest request)
        {
            try
            {
                string url = $"/api/BillStatistics?PageNumber={request.PageNumber}&PageSize={request.PageSize}";

                var result = await _httpClient.GetFromJsonAsync<List<BillStatisticDto>>(url);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<ServiceOrderStatisticDto>> GetServiceOrderStatisticsAsync(ServiceOrderStatisticRequest request)
        {
            try
            {
                string url = $"/api/ServiceOrderStatistics?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
                var result = await _httpClient.GetFromJsonAsync<List<ServiceOrderStatisticDto>>(url);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<RoomBookingStatisticDto>> GetRoomBookingStatisticsAsync(RoomBookingStatisticRequest request)
        {
            try
            {
                string url = $"/api/RoomBookingStastics?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
                var result = await _httpClient.GetFromJsonAsync<List<RoomBookingStatisticDto>>(url);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
          
        }
    }
}
