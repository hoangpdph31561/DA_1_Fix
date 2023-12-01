namespace BaseSolution.BlazorServer.Data.DataTransferObjects.AmenityRoomDetail
{
    public class AmenityRoomDetailInfo
    {
        public bool IsExist { get; set; }
        public Guid AmenityId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Amount { get; set; }
    }
}
