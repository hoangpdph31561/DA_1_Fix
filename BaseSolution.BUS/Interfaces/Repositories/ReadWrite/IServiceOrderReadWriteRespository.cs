using BaseSolution.Application.DataTransferObjects.ServiceOrder.Request;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.Interfaces.Repositories.ReadWrite
{
    public interface IServiceOrderReadWriteRespository
    {
        Task<RequestResult<Guid>> CreateNewServiceOrder(ServiceOrderEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<Guid>> CreateNewServiceOrderForRoomBooking(ServiceOrderEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<Guid>> CreateNewServiceOrderForCustomer(ServiceOrderEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateServiceOrder(ServiceOrderEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteServiceOrder(ServiceOrderDeleteRequest request, CancellationToken cancellationToken);
    }
}
