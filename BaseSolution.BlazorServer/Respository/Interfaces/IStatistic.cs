using BaseSolution.BlazorServer.Data.DataTransferObjects.Statistic;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Statistic.Request;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IStatistic
    {
        Task<List<RoomBookingStatisticDto>> GetRoomBookingStatisticsAsync(RoomBookingStatisticRequest request);
        Task<List<BillStatisticDto>> GetBillStatisticsAsync(BillStatisticRequest request);
        Task<List<ServiceOrderStatisticDto>> GetServiceOrderStatisticsAsync(ServiceOrderStatisticRequest request);

    }
}
