using BaseSolution.Application.DataTransferObjects.ServiceOrderDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;


namespace BaseSolution.Infrastructure.ViewModels.ServiceOrderDetail
{
    public class ServiceOrderCreateUpdateDeleteViewModel : ViewModelBase<List<ServiceOrderCreateUpdateDeleteRequest>>
    {
        private readonly IServiceOrderDetailReadWriteRespository _serviceOrderDetailReadWrite;
        private readonly ILocalizationService _localizationService;

        public ServiceOrderCreateUpdateDeleteViewModel(IServiceOrderDetailReadWriteRespository serviceOrderDetailReadWrite , ILocalizationService localizationService)
        {
            _serviceOrderDetailReadWrite = serviceOrderDetailReadWrite;
            _localizationService = localizationService;

        }

        public override async Task HandleAsync(List<ServiceOrderCreateUpdateDeleteRequest> request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceOrderDetailReadWrite.CreateUpdateDeleteServiceOrderDetaillAsync(request, cancellationToken);
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
                        Error = _localizationService["Error occurred while updating the ServiceOrderDetail"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "ServiceOrderDetail")
                    }
                };
            }
        }
    }
}
