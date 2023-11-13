using System.ComponentModel.DataAnnotations;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.Floor.Request
{
    public class FloorCreateRequest
    {
        [Required(ErrorMessage ="Trường bắt buộc")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Trường bắt buộc phải nhập")]
        [RegularExpression(@"^$[\d]*",ErrorMessage = "Chỉ được nhâp số")]
        public int NumberOfRoom { get; set; }
        public Guid BuildingId { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
