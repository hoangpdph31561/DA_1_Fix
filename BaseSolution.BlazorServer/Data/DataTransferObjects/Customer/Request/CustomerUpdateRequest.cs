using BaseSolution.BlazorServer.Data.ValueObjects.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Customer.Request
{
    public class CustomerUpdateRequest
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [RegularExpression(@"^[\p{L}\s]{5,}$", ErrorMessage = "Vui lòng nhập tên có ít nhất 5 ký tự")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Vui lòng nhập địa chỉ email đúng định dạng")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Vui lòng nhập số điện thoại hợp lệ.")]
        public string PhoneNumber { get; set; } = string.Empty;
        public CustomerType CustomerType { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public Guid? ModifiedBy { get; set; }

    }
}
