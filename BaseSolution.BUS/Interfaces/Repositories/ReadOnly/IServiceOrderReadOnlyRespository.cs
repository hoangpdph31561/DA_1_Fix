using BaseSolution.Application.DataTransferObjects.ServiceOrder;
using BaseSolution.Application.DataTransferObjects.ServiceOrder.Request;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.ValueObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IServiceOrderReadOnlyRespository
    {
        Task<RequestResult<ServiceOrderDTO?>> GetServiceOrderByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<ServiceOrderDTO>>> GetServicesByAdminAsync(ViewServiceOrderWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<ServiceOrderDTO>>> GetServicesByOtherAsync(ViewServiceOrderWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<List<ServiceOrderForRoomBookingDTO>>> GetServiceOrderByIdRoomBookingAsync(Guid idRoombookingDetail, CancellationToken cancellationToken);

    }
}
