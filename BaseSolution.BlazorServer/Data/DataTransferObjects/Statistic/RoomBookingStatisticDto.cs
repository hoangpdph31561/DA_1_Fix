namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Statistic
{
    public class RoomBookingStatisticDto
    {
        public int Month { get; set; } // các tháng đặt phòng
        public string NameRoom { get; set; }
        public int BookingCount { get; set; }
    }
}
