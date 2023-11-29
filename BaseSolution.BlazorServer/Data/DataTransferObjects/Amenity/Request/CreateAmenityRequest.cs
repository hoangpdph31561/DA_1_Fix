using System.ComponentModel.DataAnnotations;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Amenity.Request
{
    public class CreateAmenityRequest
    {
        [Required(ErrorMessage = "Trường này không được để trống")]
        [RegularExpression(@"^[\w\d\s]{5,}$", ErrorMessage = "Chỉ được nhập chữ hoặc số trên 5 ký tự")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Trường này không được để trống")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Phải nhập trên 5 ký tự")]
        public string Description { get; set; } = string.Empty;
        public Guid? CreatedBy { get; set; }
    }
}
