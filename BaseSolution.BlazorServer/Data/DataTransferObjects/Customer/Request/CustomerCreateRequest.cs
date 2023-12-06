using BaseSolution.BlazorServer.Data.ValueObjects.Common;
using System.ComponentModel.DataAnnotations;
namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Customer.Request
{
    public class CustomerCreateRequest
    {
        [RegularExpression(@"^[\w\s]{5,}$", ErrorMessage = "Chỉ được nhập chữ trên 5 ký tự")]
        public string Name { get; set; } = string.Empty;
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Mã định danh phải có đúng 12 chữ số")]
        public string IdentificationNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải có đúng 10 chữ số")]
        public string PhoneNumber { get; set; } = string.Empty;
        public string? ApprovedCode { get; set; }
        public DateTimeOffset? ApprovedCodeExpiredTime { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Locked;
    }
}
