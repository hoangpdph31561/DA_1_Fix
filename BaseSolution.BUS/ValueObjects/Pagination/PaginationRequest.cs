using BaseSolution.Application.ValueObjects.Common;

namespace BaseSolution.Application.ValueObjects.Pagination;

public class PaginationRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}