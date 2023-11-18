using BaseSolution.BlazorServer.Data.ValueObjects.Common;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;
using System.Text.Json.Serialization;
using static BaseSolution.BlazorServer.Data.ValueObjects.Common.QueryConstant;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.User.Request
{
    public class ViewUsersWithPaginationRequest : PaginationRequest
    {
        public SearchModel? SearchModel { get; set; } = new();
        public SortModel? SortByField { get; set; } = new();

        [JsonPropertyName("sign_up_date_start")]
        public DateTimeOffset? SignUpDateStart { get; set; }
        [JsonPropertyName("sign_up_date_end")]
        public DateTimeOffset? SignUpDateEnd { get; set; }
        [JsonPropertyName("country_iso")]
        public string? CountryISO { get; set; } = null!;
        [JsonPropertyName("sign_up_type")]
        public string? SignUpType { get; set; }
        [JsonPropertyName("is_last_24h")]
        public bool IsLast24h { get; set; } = false;
        [JsonPropertyName("cash_balance")]
        public long CashBalance { get; set; }
        [JsonPropertyName("cash_balance_comparison_operator")]
        public string? CashBalanceComparisonOperator { get; set; } = OperatorTypes.GreaterThanOrEqual;
    }
}
