using System.ComponentModel.DataAnnotations;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Building.Request
{
    public class BuildingCreateRequest
    {
        [Required(ErrorMessage ="Trường này không được để trống")]
        [RegularExpression(@"^[\w\d\s]*$",ErrorMessage ="Chỉ được nhập chữ hoặc số")]
        public string Name { get; set; } = string.Empty;
        public Guid? CreatedBy { get; set; }
    }
}
