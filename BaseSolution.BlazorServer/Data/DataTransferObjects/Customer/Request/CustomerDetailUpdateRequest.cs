using System.ComponentModel.DataAnnotations;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Customer.Request
{
    public class CustomerDetailUpdateRequest
    {
        public Guid Id { get; set; }
        [RegularExpression(@"^[a-zA-Z\s]{5,}$", ErrorMessage = "Chỉ được nhập chữ và khoảng trắng, ít nhất 5 ký tự!")]
        public string Name { get; set; } = string.Empty;
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Mã định danh phải có đúng 12 chữ số.")]
        public string IdentificationNumber { get; set; } = string.Empty;
        [RegularExpression(@"^\S+@\S+\.\S+$", ErrorMessage = "Vui lòng nhập đúng định dạng email.")]
        public string Email { get; set; } = string.Empty;
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Vui lòng nhập số điện thoại từ 10 đến 11 chữ số.")]
        public string PhoneNumber { get; set; } = string.Empty;
        public Guid? ModifiedBy { get; set; }
    }
}
