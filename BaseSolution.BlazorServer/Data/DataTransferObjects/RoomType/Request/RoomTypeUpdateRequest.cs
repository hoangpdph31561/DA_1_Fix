using BaseSolution.BlazorServer.Data.ValueObjects.Common;
using System.ComponentModel.DataAnnotations;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.RoomType.Request
{
    public class RoomTypeUpdateRequest
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        [RegularExpression(@"^[\w\d\s]{5,}$", ErrorMessage = "Chỉ được nhập chữ hoặc số trên 5 ký tự")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Trường này không được để trống")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Phải nhập trên 5 ký tự")]
        public string Description { get; set; } = string.Empty;
        public EntityStatus Status { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
