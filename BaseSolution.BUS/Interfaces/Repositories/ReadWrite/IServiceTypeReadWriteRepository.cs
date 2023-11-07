using BaseSolution.Application.DataTransferObjects.ServiceType.Request;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.Interfaces.Repositories.ReadWrite
{
    public interface IServiceTypeReadWriteRepository
    {
        Task<RequestResult<Guid>> AddServiceTypeAsync(ServiceTypeEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateServiceTypeAsync(ServiceTypeEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteServiceTypeAsync(ServiceTypeDeleteRequest request, CancellationToken cancellationToken);
    }
}
