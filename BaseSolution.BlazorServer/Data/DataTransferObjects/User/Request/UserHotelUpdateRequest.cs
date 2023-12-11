using BaseSolution.BlazorServer.Data.ValueObjects.Common;
using System.ComponentModel.DataAnnotations;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.User.Request
{
    public class UserHotelUpdateRequest
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống")]
        [EmailAddress(ErrorMessage = "Vui lòng nhập địa chỉ email đúng định dạng")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Vui lòng nhập số điện thoại hợp lệ.")]
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public Guid UserRoleId { get; set; }
        public EntityStatus Status { get; set; }
    }
}
