using System.Text.Json.Serialization;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.User
{
    public class UserBaseDto
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = null!;
        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = null!;
        [JsonPropertyName("gender")]
        public string Gender { get; set; } = null!;
        [JsonPropertyName("dob")]
        public DateTime DOB { get; set; }
        [JsonPropertyName("sign_up_type")]
        public string SignUpType { get; set; } = "None";
        [JsonPropertyName("sign_up_mac_address")]
        public string SignUpMACAddress { get; set; } = null!;
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;
        [JsonPropertyName("user_name")]
        public string UserName { get; set; } = null!;
        [JsonPropertyName("password_hash")]
        public string PasswordHash { get; set; } = null!;
        [JsonPropertyName("invite_code")]
        public string? InviteCode { get; set; } = null;
        [JsonPropertyName("parent_code")]
        public string? ParentCode { get; set; } = null;
        [JsonPropertyName("country_iso")]
        public string CountryISO { get; set; } = null!;
        [JsonPropertyName("avatar_url")]
        public string? AvatarUrl { get; set; } = null!;
        [JsonPropertyName("wallpaper_url")]
        public string? WallpaperUrl { get; set; } = null!;
        [JsonPropertyName("status")]
        public string Status { get; set; } = null!;

        [JsonPropertyName("created_time")]
        public DateTimeOffset CreatedTime { get; set; }
        [JsonPropertyName("created_by")]
        public long? CreatedBy { get; set; }
        [JsonPropertyName("modified_time")]
        public DateTimeOffset ModifiedTime { get; set; }
        [JsonPropertyName("modified_by")]
        public long? ModifiedBy { get; set; }
        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }
        [JsonPropertyName("deleted_by")]
        public long? DeletedBy { get; set; }
        [JsonPropertyName("deleted_time")]
        public DateTimeOffset DeletedTime { get; set; }
    }
}
