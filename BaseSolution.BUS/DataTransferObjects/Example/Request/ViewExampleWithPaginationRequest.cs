using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Domain.Enums;

namespace BaseSolution.Application.DataTransferObjects.Example.Request
{
    public class ViewExampleWithPaginationRequest : PaginationRequest
    {
        // SearchFields
        public string? Name { get; set; }

        // SortFields
        public EntityStatus? Status { get; set; }
    }
}
