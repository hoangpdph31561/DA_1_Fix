using System.Text.Json.Serialization;
namespace BaseSolution.BlazorServer.Data.DataTransferObjects.User
{
    public class UserDto : UserBaseDto
    {
        [JsonPropertyName("is_kyc")]
        public bool IsKYC { get; set; }
        [JsonPropertyName("claim_cash")]
        public long ClaimCash { get; set; }
        [JsonPropertyName("expected_claim_time")]
        public int ExpectedClaimTime { get; set; }
        [JsonPropertyName("actual_claim_time")]
        public int ActualClaimTime { get; set; }
        [JsonPropertyName("contributed_claim_time")]
        public int ContributedClaimTime { get; set; }
    }
}
