
using Azure.Core;
using BaseSolution.Application.DataTransferObjects.ServiceOrder.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Infrastructure.Implements.Repositories.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.ServiceOrder
{
    public class ServiceOrderDeleteViewModel : ViewModelBase<ServiceOrderDeleteRequest>
    {
        private readonly ILocalizationService _localizationService;
        private readonly IServiceOrderReadWriteRespository _serviceOrderReadWrite;
        public ServiceOrderDeleteViewModel(IServiceOrderReadWriteRespository serviceOrderReadWrite, ILocalizationService localizationService)
        {
            _serviceOrderReadWrite = serviceOrderReadWrite;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(ServiceOrderDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceOrderReadWrite.DeleteServiceOrder(request, cancellationToken);

                Success = result.Success;
                ErrorItems = result.Errors;
                Message = result.Message;
                return;
            }
            catch (Exception)
            {
                Success = false;
                ErrorItems = new[]
                    {
                    new ErrorItem
                    {
                        Error = _localizationService["Error occurred while updating the service order"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "service order")
                    }
                };
            }
        }
    }
}
