using BaseSolution.Application.ValueObjects.Pagination;

namespace BaseSolution.Application.DataTransferObjects.User.Request
{
    public class ViewUserWithPaginationRequest : PaginationRequest
    {
        public string? Name { get; set; }

        public Guid? UserRoleId { get; set; }


    }
}
