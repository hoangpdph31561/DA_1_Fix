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
                    var serviceOrderDetail = _dbContext.ServiceOrderDetails
                        .Where(x => x.ServiceOrderId == item.ServiceOrderId && !x.Deleted)
                        .ToList();

                    item.TotalAmountForService = serviceOrderDetail.Sum(s => s.Price * (decimal)s.Amount);
                    item.ServiceAmountForRoom = item.TotalAmountForService;

                    item.RoomAmount = UtilityExtensions.TinhTien(item.CheckInReality, item.CheckOutReality, item.RoomPrice, item.PrePaid);
                    item.TotalAmount = item.ServiceAmountForRoom + item.RoomAmount;
                    item.TotalAll = item.TotalAmount + item.TotalAmountForService;
                }

                List<BillStatisticDto> lstTepRests = null;
                List<BillStatisticDto> totalAmountForMonth = null;

                if (lstRests != null)
                {
                    lstTepRests = lstRests.GroupBy(c => new
                    {
                        c.Month,
                        c.TotalAll,
                    })
              .Select(gcs => new BillStatisticDto()
              {
                  Month = gcs.Key.Month,
                  TotalAll = gcs.Key.TotalAll
              }).ToList();

                     totalAmountForMonth = lstTepRests.GroupBy(x => x.Month)
                                    .Select(g => new BillStatisticDto
                                    {
                                        Month = g.Key,
                                        TotalAll = g.Sum(x => x.TotalAll)
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
