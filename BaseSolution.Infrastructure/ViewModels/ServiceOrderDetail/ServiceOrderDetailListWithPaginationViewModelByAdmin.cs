using BaseSolution.Application.DataTransferObjects.ServiceOrderDetail.Request;
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

namespace BaseSolution.Infrastructure.ViewModels.ServiceOrderDetail
{
    public class ServiceOrderDetailListWithPaginationViewModelByAdmin : ViewModelBase<ViewServiceOrderDetailWithPaginationRequest>
    {
        private readonly IServiceOrderDetailReadOnlyRespository _serviceOrderDetailReadOnly;
        private readonly ILocalizationService _localizationService;
        public ServiceOrderDetailListWithPaginationViewModelByAdmin(IServiceOrderDetailReadOnlyRespository serviceOrderDetailReadOnly, ILocalizationService localizationService)
        {
            _serviceOrderDetailReadOnly = serviceOrderDetailReadOnly;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(ViewServiceOrderDetailWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceOrderDetailReadOnly.GetServiceOrderDetailsByAdminAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the list of Example"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of Example")
                    }
                };
            }
        }
    }
}
