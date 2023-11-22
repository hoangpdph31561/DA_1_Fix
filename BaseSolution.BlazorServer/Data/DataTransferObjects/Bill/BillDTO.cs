using BaseSolution.BlazorServer.Data.ValueObjects.Common;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Bill
{
    public class BillDTO
    {
        public Guid Id { get; set; } // mã hóa đơn
        public DateTimeOffset CreatedTime { get; set; }
        public Guid? RoomBookingId { get; set; }
        public Guid? ServiceOrderId { get; set; }
        public float TotalAmount { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
    }
}
