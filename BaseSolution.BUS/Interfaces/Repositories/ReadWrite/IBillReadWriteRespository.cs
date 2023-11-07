using BaseSolution.Application.DataTransferObjects.Bill.Request;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.Interfaces.Repositories.ReadWrite
{
    public interface IBillReadWriteRespository
    {
        Task<RequestResult<Guid>> CreateNewBill (BillEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateBill (BillEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteBill (BillDeleteRequest request, CancellationToken cancellationToken);
    }
}
