namespace BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrder
{
    public class ServiceOrderForRoomBookingDTO
    {
        
            public Guid ServiceId { get; set; }
            public string ServiceName { get; set; }
            public Guid RoomBookingDetailId { get; set; } // đặt dịch vụ cho phòng  
            public int Quantity { get; set; }
        public Guid CustomerId { get; set; }
    }
}
