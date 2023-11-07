using BaseSolution.Application.DataTransferObjects.Services.Request;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.Interfaces.Repositories.ReadWrite
{
    public interface IServicesReadWriteRepository
    {
        Task<RequestResult<Guid>> AddServiceAsync(ServiceEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateServiceAsync(ServiceEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteServiceAsync(ServiceDeleteRequest request, CancellationToken cancellationToken);
    }
}
