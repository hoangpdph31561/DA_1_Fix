using BaseSolution.Application.DataTransferObjects.Statistic.Bill.Request;
using BaseSolution.Application.DataTransferObjects.Statistic.ServiceOrder;
using BaseSolution.Application.DataTransferObjects.Statistic.ServiceOrder.Request;
using BaseSolution.Application.ValueObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IServiceOrderStatisticReadOnlyRespository
    {
        Task<RequestResult<List<ServiceOrderStatisticDto>>> GetServiceOrderAsync(
      ViewServiceOrderWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
