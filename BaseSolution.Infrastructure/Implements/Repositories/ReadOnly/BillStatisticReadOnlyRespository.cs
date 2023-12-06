using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseSolution.Application.DataTransferObjects.Statistic.Bill;
using BaseSolution.Application.DataTransferObjects.Statistic.Bill.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Enums;
using BaseSolution.Infrastructure.Database.AppDbContext;
using BaseSolution.Infrastructure.Extensions;

namespace BaseSolution.Infrastructure.Implements.Repositories.ReadOnly
{
    public class BillStatisticReadOnlyRespository : IBillStatisticReadOnlyRespository
    {
        private readonly AppReadOnlyDbContext _dbContext;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        public BillStatisticReadOnlyRespository(AppReadOnlyDbContext dbContext, ILocalizationService localizationService, IMapper mapper)
        {
            _dbContext = dbContext;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public async Task<RequestResult<List<BillStatisticDto>>> GetBillStasticAsync(BillBillStatisticRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = await _dbContext.Bills.Where(x => x.Status != EntityStatus.Deleted).ProjectTo<BillStatisticDto>(_mapper.ConfigurationProvider).PaginateAsync(request, cancellationToken);
                List<BillStatisticDto> lstRests = query.Data!.ToList();

                foreach (var item in lstRests)
                {
                    item.RoomAmount = (float)item.RoomPrice * item.TotalRoom;
                    item.ServiceAmount = (float)item.ServicePrice * item.TotalService;
                    item.TotalAmount = item.ServiceAmount + (float)item.RoomPrice;
                }
                List<BillStatisticDto> lstTepRests = null;
                List<BillStatisticDto> totalAmountForMonth = null;

                if (lstRests != null)
                {
                    lstTepRests = lstRests.GroupBy(c => new
                    {
                        c.Month,
                        c.RoomPrice,
                        c.ServicePrice,
                        c.TotalService,
                        c.TotalRoom,
                        c.ServiceAmount,
                        c.TotalAmount,
                        c.RoomAmount
                    })
              .Select(gcs => new BillStatisticDto()
              {
                  Month = gcs.Key.Month,
                  RoomPrice = gcs.Key.RoomPrice,
                  ServicePrice = gcs.Key.ServicePrice,
                  TotalService = gcs.Key.TotalService,
                  TotalRoom = gcs.Key.TotalRoom,
                  ServiceAmount = gcs.Key.ServiceAmount,
                  TotalAmount = gcs.Key.TotalAmount,
                  RoomAmount = gcs.Key.RoomAmount,
                  TotalAmountForMonth = (int)gcs.Sum(x => x.TotalAmount),
              }).OrderBy(x => x.Month).ToList();
                

                     totalAmountForMonth = lstTepRests.GroupBy(x => x.Month)
                                    .Select(g => new BillStatisticDto
                                    {
                                        Month = g.Key,
                                        TotalAmountForMonth = g.Sum(x => x.TotalAmountForMonth)
                                    }).ToList();


                }
                return RequestResult<List<BillStatisticDto>>.Succeed(totalAmountForMonth);

            }
            catch (Exception e)
            {

                return RequestResult<List<BillStatisticDto>>.Fail(_localizationService["bill is not found"], new[]
                    {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "bill"
                    }
                });
            }

        }
    }
}
