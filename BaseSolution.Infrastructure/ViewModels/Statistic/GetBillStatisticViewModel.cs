using BaseSolution.Application.DataTransferObjects.Statistic.Bill.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Infrastructure.Implements.Repositories.ReadOnly;
using BaseSolution.Infrastructure.Implements.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.Statistic
{
    public class GetBillStatisticViewModel : ViewModelBase<BillBillStatisticRequest>
    {
        private readonly IBillStatisticReadOnlyRespository _billStatisticReadOnly;
        private readonly ILocalizationService _localizationService;

        public GetBillStatisticViewModel(IBillStatisticReadOnlyRespository billStatisticReadOnly , ILocalizationService localizationService)
        {
            _billStatisticReadOnly = billStatisticReadOnly;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(BillBillStatisticRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _billStatisticReadOnly.GetBillStasticAsync(request, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the list of bill"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of bill")
                }
            };
            }
        }
    }
}
