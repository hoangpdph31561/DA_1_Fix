using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseSolution.Application.DataTransferObjects.Statistic.ServiceOrder;
using BaseSolution.Application.DataTransferObjects.Statistic.ServiceOrder.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Enums;
using BaseSolution.Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Implements.Repositories.ReadOnly
{
    public class ServiceOrderStatisticReadOnlyRespository : IServiceOrderStatisticReadOnlyRespository
    {
        private readonly AppReadOnlyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public ServiceOrderStatisticReadOnlyRespository(AppReadOnlyDbContext dbContext, ILocalizationService localizationService, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<List<ServiceOrderStatisticDto>>> GetServiceOrderAsync(ViewServiceOrderWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _dbContext.ServiceOrders.AsNoTracking().Where(x => x.Deleted == false && x.Status != EntityStatus.Deleted)
              .ProjectTo<ServiceOrderStatisticDto>(_mapper.ConfigurationProvider);

                List<ServiceOrderStatisticDto> lstRests = await query.ToListAsync();
                List<ServiceOrderStatisticDto> lstTepRests = null;

                if (lstRests != null)
                {
                    lstTepRests = lstRests.GroupBy(c => new
                    {
                        c.NameService,
                        c.Month
                    })

                    .Select(gcs => new ServiceOrderStatisticDto()
                    {
                        NameService = gcs.Key.NameService,
                        Month = gcs.Key.Month,
                        OrderCount = gcs.Count(),
                    }).OrderBy(x => x.Month).ToList();
                }
                var serviceWithMaxBookings = lstTepRests.GroupBy(x => x.Month)
                                                 .Select(g => g.First(x => x.OrderCount == g.Max(y => y.OrderCount))).ToList();
                return RequestResult<List<ServiceOrderStatisticDto>>.Succeed(serviceWithMaxBookings);
            }
            catch (Exception e)
            {

                return RequestResult<List<ServiceOrderStatisticDto>>.Fail(_localizationService["ServiceOrder is not found"], new[]
                     {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "ServiceOrder"
                    }
                });
            }

        }
    }
}
