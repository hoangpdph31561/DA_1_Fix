
using BaseSolution.Application.DataTransferObjects.ServiceOrder.Request;
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

namespace BaseSolution.Infrastructure.ViewModels.ServiceOrder
{
    public class ServiceOrderListWithPaginationViewModelByOther : ViewModelBase<ViewServiceOrderWithPaginationRequest>
    {
        private readonly IServiceOrderReadOnlyRespository _serviceOrderReadOnly;
        private readonly ILocalizationService _localizationService;
        public ServiceOrderListWithPaginationViewModelByOther(IServiceOrderReadOnlyRespository serviceOrderReadOnly, ILocalizationService localizationService)
        {
            _serviceOrderReadOnly = serviceOrderReadOnly;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(ViewServiceOrderWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceOrderReadOnly.GetServicesByOtherAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the list of service order"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of service order")
                    }
                };
            }
        }
    }
}
