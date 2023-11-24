using BaseSolution.BlazorServer.Data.DataTransferObjects.AmenityRoomDetail;
using BaseSolution.BlazorServer.Data.DataTransferObjects.AmenityRoomDetail.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IAmenityRoomDetailRepo
    {
        Task<PaginationResponse<AmenityRoomDetailDTO>> GetAllAmentityRoomDetail(ViewAmenityRoomDetailWithPaginationRequest request);
    }
}
