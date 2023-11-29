using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Data.DataTransferObjects.User.Request
{
    public class ViewUsersHotelWithPaginationRequest : PaginationRequest
    {

        public string? Name { get; set; }
        public Guid? UserRoleId { get; set; }
    }
}
