using BaseSolution.BlazorServer.Data.ValueObjects.Common;
using System.ComponentModel.DataAnnotations;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBooking.Request
{
    public class RoombookingCreateRequest
    {
        public Guid CustomerId { get; set; }
        public Guid Id { get; set; }
        public Guid RoomDetailId { get; set; }
        public BookingType BookingType { get; set; }
        public string CodeBooking { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        public DateTimeOffset CheckInBooking { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        public DateTimeOffset CheckOutBooking { get; set; }
        public decimal Price { get; set; }

    }
}
