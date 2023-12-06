using AutoMapper;
using BaseSolution.Application.DataTransferObjects.ServiceOrder.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.ViewModels.ServiceOrder
{
    public class CreateNewServiceOrderForRoomBookingVM : ViewModelBase<ServiceOrderCreateForRoomBookingRequest>
    {
        private readonly IServiceOrderReadOnlyRespository _serviceOrderReadOnly;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        private readonly IServiceOrderReadWriteRespository _serviceOrderReadWrite;
        public CreateNewServiceOrderForRoomBookingVM(IServiceOrderReadOnlyRespository serviceOrderReadOnly, IServiceOrderReadWriteRespository serviceOrderReadWrite, IMapper mapper, ILocalizationService localizationService)
        {
            _serviceOrderReadOnly = serviceOrderReadOnly;
            _localizationService = localizationService;
            _mapper = mapper;
            _serviceOrderReadWrite = serviceOrderReadWrite;
        }
        public override async Task HandleAsync(ServiceOrderCreateForRoomBookingRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var createServiceOrder = await _serviceOrderReadWrite.CreateNewServiceOrderForRoomBooking(_mapper.Map<ServiceOrderEntity>(request), cancellationToken);

                if (createServiceOrder.Success)
                {
                    var result = await _serviceOrderReadOnly.GetServiceOrderByIdAsync(createServiceOrder.Data, cancellationToken);

                    Data = result.Data!;
                    Success = result.Success;
                    ErrorItems = result.Errors;
                    Message = result.Message;
                    return;
                }

                Success = createServiceOrder.Success;
                ErrorItems = createServiceOrder.Errors;
                Message = createServiceOrder.Message;
            }
            catch (Exception)
            {
                Success = false;
                ErrorItems = new[]
                    {
                    new ErrorItem
                    {
                        Error = _localizationService["Error occurred while getting the service order"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "service order")
                    }
                };
            }
        }
    }
}
