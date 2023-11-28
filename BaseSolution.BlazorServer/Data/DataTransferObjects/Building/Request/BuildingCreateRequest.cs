using System.ComponentModel.DataAnnotations;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Building.Request
{
    public class BuildingCreateRequest
    {
        [Required(ErrorMessage ="Trường này không được để trống")]
        [RegularExpression(@"^[\w\d\s]{5,}$",ErrorMessage ="Chỉ được nhập chữ hoặc số trên 5 ký tự")]
        public string Name { get; set; } = string.Empty;
        public Guid? CreatedBy { get; set; }
    }
}
