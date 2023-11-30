using BaseSolution.BlazorServer.Data.ValueObjects.Common;
using System.ComponentModel.DataAnnotations;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Service.Request
{
    public class ServiceUpdateRequest
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        [RegularExpression(@"^[\w\d\s]{5,}$", ErrorMessage = "Chỉ được nhập chữ hoặc số trên 5 ký tự")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Trường này không được để trống")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Phải nhập trên 5 ký tự")]
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string Unit { get; set; } = string.Empty;
        public bool IsRoomBookingNeed { get; set; }
        public EntityStatus Status { get; set; }
        public Guid? ModifiedBy { get; set; }
        public Guid ServiceTypeId { get; set; }
    }
}
