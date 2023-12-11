using System.ComponentModel.DataAnnotations;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Customer.Request
{
    public class CustomerDetailUpdateRequest
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [RegularExpression(@"^[\p{L}\s]{5,}$", ErrorMessage = "Vui lòng nhập tên có ít nhất 5 ký tự")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Mã định danh không được để trống")]
        public string IdentificationNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Trường này không được để trống")]
        [EmailAddress(ErrorMessage = "Vui lòng nhập địa chỉ email đúng định dạng")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Vui lòng nhập số điện thoại hợp lệ.")]
        public string PhoneNumber { get; set; } = string.Empty;
        public Guid? ModifiedBy { get; set; }
    }
}
