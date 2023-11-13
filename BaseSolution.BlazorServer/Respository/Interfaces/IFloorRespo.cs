using BaseSolution.BlazorServer.Data.DataTransferObjects.Floor;
using BaseSolution.BlazorServer.Data.DataTransferObjects.Floor.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IFloorRespo
    {
        Task<PaginationResponse<FloorDTO>> GetAllFloors(ViewFloorWithPaginationRequest request);
        Task<bool> CreateNewFloor (FloorCreateRequest request);
        Task<bool> UpdateFloor(FloorUpdateRequest request);
        Task<bool> DeleteFloor(FloorDeleteRequest request);
        Task<FloorDTO> GetFloorById(Guid id);
    }
}
