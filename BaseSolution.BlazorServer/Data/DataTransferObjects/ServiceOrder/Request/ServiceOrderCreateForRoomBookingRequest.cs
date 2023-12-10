namespace BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrder.Request
{
    public class ServiceOrderCreateForRoomBookingRequest
    {
        public Guid RoomBookingDetailId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Price { get; set; }
    }
}
