using BaseSolution.BlazorServer.Data.DataTransferObjects.AmenityRoomDetail;
using BaseSolution.BlazorServer.Data.DataTransferObjects.AmenityRoomDetail.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IAmenityRoomDetailRespo
    {
        Task<PaginationResponse<AmenityRoomDetailDTO>> GetAllAmentityRoomDetail(ViewAmenityRoomDetailWithPaginationRequest request);
        Task<bool> CreateUpdateDeleteAmenityRoomDetail(List<AmenityRoomDetailCreateUpdateDelete> request);
        Task<PaginationResponse<AmenityRoomDetailDTO>> GetAmenityRoomDetailByAmenityId(ViewAmenityRoomDetailWithPaginationRequestAndAmenityId request);
    }
}
