using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.ServiceOrder
{
    public class ServiceOrderListWithPaginationByIdRoomBookingViewModel : ViewModelBase<Guid>
    {
        private readonly IServiceOrderReadOnlyRespository _serviceOrderReadOnly;
        private readonly ILocalizationService _localizationService;
        public ServiceOrderListWithPaginationByIdRoomBookingViewModel(IServiceOrderReadOnlyRespository serviceOrderReadOnly, ILocalizationService localizationService)
        {
            _serviceOrderReadOnly = serviceOrderReadOnly;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(Guid iDRoomBooking, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceOrderReadOnly.GetServiceOrderByIdRoomBookingAsync(iDRoomBooking, cancellationToken);

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
