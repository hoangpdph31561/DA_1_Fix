using BaseSolution.Application.DataTransferObjects.Statistic.ServiceOrder.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Infrastructure.Implements.Repositories.ReadOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.Statistic
{
    public class GetServiceOrderStatisticViewModel : ViewModelBase<ViewServiceOrderWithPaginationRequest>
    {
        private readonly IServiceOrderStatisticReadOnlyRespository _serviceStatisticReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public GetServiceOrderStatisticViewModel(IServiceOrderStatisticReadOnlyRespository serviceStatisticReadOnlyRepository, ILocalizationService localizationService)
        {
            _serviceStatisticReadOnlyRepository = serviceStatisticReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(ViewServiceOrderWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceStatisticReadOnlyRepository.GetServiceOrderAsync(request, cancellationToken);

                Data = result.Data!;
                Success = result.Success;
                ErrorItems = result.Errors;
                Message = result.Message;
                return;
            }
            catch
            {
                Success = false;
                ErrorItems = new[]
                {
                new ErrorItem
                {
                    Error = _localizationService["Error occurred while getting the list of serviceOrder"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of serviceOrder")
                }
            };
            }
        }
    }
}
