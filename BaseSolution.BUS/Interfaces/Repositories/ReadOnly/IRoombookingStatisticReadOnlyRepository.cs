using BaseSolution.Application.DataTransferObjects.Statistic.RoomBooking;
using BaseSolution.Application.DataTransferObjects.Statistic.RoomBooking.Request;
using BaseSolution.Application.ValueObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Application.Interfaces.Repositories.ReadOnly
{
    public interface IRoombookingStatisticReadOnlyRepository
    {
        Task<RequestResult<List<RoomBookingStatisticDto>>> GetRoomBookingStasticAsync(
         ViewRoomBookingWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
