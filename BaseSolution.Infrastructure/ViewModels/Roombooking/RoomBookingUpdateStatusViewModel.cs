using AutoMapper;
using BaseSolution.Application.DataTransferObjects.RoomBooking.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.Roombooking
{
    public class RoomBookingUpdateStatusViewModel : ViewModelBase<RoomBookingUpdateStatusRequest>
    {
        private readonly IRoombookingReadWriteRepository _roombookingReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public RoomBookingUpdateStatusViewModel(IRoombookingReadWriteRepository roombookingReadWriteRepository, IMapper mapper, ILocalizationService localizationService)
        {
            _roombookingReadWriteRespository = roombookingReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public override async Task HandleAsync(RoomBookingUpdateStatusRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roombookingReadWriteRespository.UpdateStatusRoomBookingAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the RoomBooking"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "RoomBooking")
                    }
                };
            }
        }
    }
}
