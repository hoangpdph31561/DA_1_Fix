using System.ComponentModel.DataAnnotations;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Floor.Request
{
    public class FloorCreateRequest
    {
        [Required(ErrorMessage = "Trường này không được để trống")]
        [RegularExpression(@"^[\w\d\s]{5,}$", ErrorMessage = "Chỉ được nhập chữ hoặc số trên 5 ký tự")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Trường bắt buộc phải nhập")]
        public int NumberOfRoom { get; set; }
        [Required(ErrorMessage = "Trường bắt buộc")]
        public Guid BuildingId { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
