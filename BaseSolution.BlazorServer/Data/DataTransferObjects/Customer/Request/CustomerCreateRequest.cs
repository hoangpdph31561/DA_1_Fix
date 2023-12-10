using BaseSolution.BlazorServer.Data.ValueObjects.Common;
using System.ComponentModel.DataAnnotations;
namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Customer.Request
{
    public class CustomerCreateRequest
    {
        [Required(ErrorMessage = "Trường này không được để trống")]
        [RegularExpression(@"^[\p{L}\s]{5,}$", ErrorMessage = "Vui lòng nhập tên có ít nhất 5 ký tự")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Trường này không được để trống")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Mã định danh phải có đúng 12 chữ số.")]
        public string IdentificationNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Trường này không được để trống")]
        [EmailAddress(ErrorMessage = "Vui lòng nhập địa chỉ email đúng định dạng")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Trường này không được để trống")]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Vui lòng nhập số điện thoại từ 10 đến 11 chữ số.")]
        public string PhoneNumber { get; set; } = string.Empty;
        public string? ApprovedCode { get; set; }
        public DateTimeOffset? ApprovedCodeExpiredTime { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Locked;
    }
}
