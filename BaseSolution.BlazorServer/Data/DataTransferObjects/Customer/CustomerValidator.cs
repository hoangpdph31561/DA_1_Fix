using BaseSolution.BlazorServer.Data.DataTransferObjects.Customer.Request;
using FluentValidation;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Customer
{
    public class CustomerCreateRequestValidator : AbstractValidator<CustomerCreateRequest>
    {
        public CustomerCreateRequestValidator() 
        {
            RuleFor(x => x.Name).MinimumLength(10).WithMessage("Tên phải có ít nhất 10 ký tự");
            RuleFor(x => x.PhoneNumber).MinimumLength(11).WithMessage("Số điện thoại phải có ít nhất 11 chữ số");
            RuleFor(x => x.IdentificationNumber).Matches(@"^\d{13}$").WithMessage("Mã định danh phải có ít nhất 13 chữ số");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Vui lòng nhập địa chỉ email")
                           .EmailAddress().WithMessage("Địa chỉ email không hợp lệ");
        }
    }
}
