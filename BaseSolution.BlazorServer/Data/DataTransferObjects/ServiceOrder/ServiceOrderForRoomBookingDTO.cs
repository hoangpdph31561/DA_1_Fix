using BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrderDetail;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrder
{
    public class ServiceOrderForRoomBookingDTO
    {

        public Guid ServiceId { get; set; }
        public Guid Id { get; set; }
        public string ServiceName { get; set; }
        public Guid RoomBookingDetailId { get; set; } // đặt dịch vụ cho phòng  
        public List<ServiceOrderDetailDto> lstServiceOrder { get; set; }
        public Guid CustomerId { get; set; }
    }
}
