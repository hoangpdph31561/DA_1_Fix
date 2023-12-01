using BaseSolution.Application.DataTransferObjects.ServiceOrderDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;


namespace BaseSolution.Infrastructure.ViewModels.ServiceOrderDetail
{
    public class ServiceOrderDetailViewModelByServiceOrderId : ViewModelBase<ViewServiceOrderDetailByIdServiceOderRequest>
    {
        private readonly IServiceOrderDetailReadOnlyRespository _serviceOrderDetailReadOnly;
        private readonly ILocalizationService _localizationService;

        public ServiceOrderDetailViewModelByServiceOrderId(IServiceOrderDetailReadOnlyRespository serviceOrderDetailReadOnly , ILocalizationService localizationService)
        {
            _serviceOrderDetailReadOnly = serviceOrderDetailReadOnly;
            _localizationService = localizationService;
        }

        public async override Task HandleAsync(ViewServiceOrderDetailByIdServiceOderRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceOrderDetailReadOnly.GetServiceOrderDetailsByServiceOrderAsync(data, cancellationToken);
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
                        Error = _localizationService["Error occurred while updating the service order detail"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "service order detail")
                    }
                };
            }
        }
    }
}
