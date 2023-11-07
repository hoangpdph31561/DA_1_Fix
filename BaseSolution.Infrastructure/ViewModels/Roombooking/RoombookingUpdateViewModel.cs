using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Roombooking.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.ViewModels.Roombooking
{
    public class RoombookingUpdateViewModel : ViewModelBase<RoombookingUpdateRequest>
    {
        private readonly IRoombookingReadWriteRepository _roombookingReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public RoombookingUpdateViewModel(IRoombookingReadWriteRepository roombookingReadWriteRepository, IMapper mapper, ILocalizationService localizationService)
        {
            _roombookingReadWriteRespository = roombookingReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(RoombookingUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roombookingReadWriteRespository.UpdateRoomBookingAsync(_mapper.Map<RoomBookingEntity>(request), cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the RoomBooking"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "RoomBooking")
                    }
                };
            }
        }
    }
}
