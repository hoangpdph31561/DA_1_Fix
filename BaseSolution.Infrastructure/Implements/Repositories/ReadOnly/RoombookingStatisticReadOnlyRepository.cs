using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseSolution.Application.DataTransferObjects.Statistic.RoomBooking;
using BaseSolution.Application.DataTransferObjects.Statistic.RoomBooking.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Enums;
using BaseSolution.Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace BaseSolution.Infrastructure.Implements.Repositories.ReadOnly
{
    public class RoombookingStatisticReadOnlyRepository : IRoombookingStatisticReadOnlyRepository
    {
        private readonly AppReadOnlyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public RoombookingStatisticReadOnlyRepository(AppReadOnlyDbContext dbContext, ILocalizationService localizationService, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<List<RoomBookingStatisticDto>>> GetRoomBookingStasticAsync(ViewRoomBookingWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _dbContext.RoomBookings.AsNoTracking().Where(x => x.Deleted == false && x.Status != EntityStatus.Deleted)
              .ProjectTo<RoomBookingStatisticDto>(_mapper.ConfigurationProvider);

                List<RoomBookingStatisticDto> lstRests = await query.ToListAsync();
                List<RoomBookingStatisticDto> lstTepRests = null;

                if (lstRests != null)
                {
                    lstTepRests = lstRests.GroupBy(c => new
                    {
                        c.NameRoom,
                        c.Month
                    })

                    .Select(gcs => new RoomBookingStatisticDto()
                    {
                      NameRoom = gcs.Key.NameRoom,
                      Month = gcs.Key.Month,
                      BookingCount = gcs.Count(),
              }).OrderBy(x => x.Month).ToList();
                }
                var roomWithMaxBookings = lstTepRests.GroupBy(x => x.Month)
                                                 .Select(g => g.First(x => x.BookingCount == g.Max(y => y.BookingCount))).ToList();
                return RequestResult<List<RoomBookingStatisticDto>>.Succeed(roomWithMaxBookings);
            }
            catch (Exception e)
            {

                return RequestResult<List<RoomBookingStatisticDto>>.Fail(_localizationService["Roombooking is not found"], new[]
                     {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "roombooking"
                    }
                });
            }

        }
    }
}
