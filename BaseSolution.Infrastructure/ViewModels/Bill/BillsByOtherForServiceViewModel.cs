using BaseSolution.Application.DataTransferObjects.Bill.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.Bill
{
    public class BillsByOtherForServiceViewModel : ViewModelBase<ViewBillWithPaginationRequest>
    {
        private readonly IBillReadOnlyRespository _billReadOnlyRespository;
        private readonly ILocalizationService _localizationService;
        public BillsByOtherForServiceViewModel(IBillReadOnlyRespository billReadOnlyRespository, ILocalizationService localizationService)
        {
            _billReadOnlyRespository = billReadOnlyRespository;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(ViewBillWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _billReadOnlyRespository.GetBillsByOtherForService(request, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the list of Bill"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of Bill")
                }
            };
            }
        }
    }
}
