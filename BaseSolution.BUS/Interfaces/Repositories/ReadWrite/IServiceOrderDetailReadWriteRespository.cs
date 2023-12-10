using BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request;
using BaseSolution.Application.DataTransferObjects.ServiceOrder.Request;
using BaseSolution.Application.DataTransferObjects.ServiceOrderDetail.Request;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.Interfaces.Repositories.ReadWrite
{
    public interface IServiceOrderDetailReadWriteRespository
    {
        Task<RequestResult<Guid>> CreateNewServiceOrderDetail(ServiceOrderDetailEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateServiceOrderDetail(ServiceOrderDetailEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteServiceOrderDetail(ServiceOrderDetailDeleteRequest request, CancellationToken cancellationToken);
        Task<RequestResult<int>> CreateUpdateDeleteServiceOrderDetaillAsync(List<ServiceOrderCreateUpdateDeleteRequest> request, CancellationToken cancellationToken);

    }
}
