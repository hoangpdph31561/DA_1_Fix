namespace BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

public class PaginationResponse<TDataType>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public bool HasNext { get; set; }
    public ICollection<TDataType>? Data { get; set; }
}