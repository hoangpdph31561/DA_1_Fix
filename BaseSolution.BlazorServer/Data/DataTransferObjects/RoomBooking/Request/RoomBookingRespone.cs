using Newtonsoft.Json;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.RoomBooking.Request
{
    public class RoomBookingRespone
    {
        [JsonProperty("data")]
        public Guid Data { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("errors")]
        public List<string> Errors { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
