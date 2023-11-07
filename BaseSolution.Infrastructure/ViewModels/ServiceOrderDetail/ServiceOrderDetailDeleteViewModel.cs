
using Azure.Core;
using BaseSolution.Application.DataTransferObjects.ServiceOrderDetail.Request;
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

namespace BaseSolution.Infrastructure.ViewModels.ServiceOrderDetail
{
    public class ServiceOrderDetailDeleteViewModel : ViewModelBase<ServiceOrderDetailDeleteRequest>
    {
        private readonly ILocalizationService _localizationService;
        private readonly IServiceOrderDetailReadWriteRespository _serviceOrderDetailReadWrite;
        public ServiceOrderDetailDeleteViewModel(IServiceOrderDetailReadWriteRespository serviceOrderDetailReadWrite, ILocalizationService localizationService)
        {
            _serviceOrderDetailReadWrite = serviceOrderDetailReadWrite;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(ServiceOrderDetailDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceOrderDetailReadWrite.DeleteServiceOrderDetail(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the service order detail"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "service order detail")
                    }
                };
            }
        }
    }
}
