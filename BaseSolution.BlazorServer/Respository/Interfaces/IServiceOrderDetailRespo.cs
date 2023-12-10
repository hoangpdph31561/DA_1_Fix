using BaseSolution.BlazorServer.Data.DataTransferObjects.AmenityRoomDetail.Request;
using BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrderDetail;
using BaseSolution.BlazorServer.Data.DataTransferObjects.ServiceOrderDetail.Request;
using BaseSolution.BlazorServer.Data.ValueObjects.Pagination;

namespace BaseSolution.BlazorServer.Respository.Interfaces
{
    public interface IServiceOrderDetailRespo
    {
        Task<bool> CreateUpdateDeleteServiceOrder(List<ServiceOrderDetailCreateUpdateDelete> request);
        Task<List<ServiceOrderDetailDto>> GetServiceOrderDetailByServiceOrderId(ViewServiceOrderDetailByIdServiceOderRequest request);

    }
}
