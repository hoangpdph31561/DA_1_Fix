using BaseSolution.Application.DataTransferObjects.Statistic.Bill;
using BaseSolution.Application.DataTransferObjects.Statistic.Bill.Request;
using BaseSolution.Application.DataTransferObjects.Statistic.RoomBooking;
using BaseSolution.Application.ValueObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IBillStatisticReadOnlyRespository
    {
        Task<RequestResult<List<BillStatisticDto>>> GetBillStasticAsync(
        BillBillStatisticRequest request, CancellationToken cancellationToken);
    }
}
