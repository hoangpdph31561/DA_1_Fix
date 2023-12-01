using BaseSolution.Application.DataTransferObjects.Statistic.Bill;
using BaseSolution.Application.DataTransferObjects.Statistic.Bill.Request;
using BaseSolution.Application.ValueObjects.Response;


namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IBillStatisticReadOnlyRespository
    {
        Task<RequestResult<List<BillStatisticDto>>> GetBillStasticAsync(
        BillBillStatisticRequest request, CancellationToken cancellationToken);
    }
}
