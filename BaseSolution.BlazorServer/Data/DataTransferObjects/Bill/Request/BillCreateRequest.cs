namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Bill.Request
{
    public class BillCreateRequest
    {
        public Guid CustomerId { get; set; }
        public Guid? ServiceOrderId { get; set; }
        public Guid? RoomBookingId { get; set; }
    }
}
