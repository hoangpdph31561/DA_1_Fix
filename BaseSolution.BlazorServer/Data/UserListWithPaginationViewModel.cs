using BaseSolution.BlazorServer.Data.DataTransferObjects.User;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Data
{
    public class UserListWithPaginationViewModel
    {
        public PaginationResponse<UserDto>? Data { get; set; }
    }
}
