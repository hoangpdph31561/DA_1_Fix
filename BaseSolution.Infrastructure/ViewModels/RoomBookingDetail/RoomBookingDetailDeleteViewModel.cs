using AutoMapper;
using BaseSolution.Application.DataTransferObjects.RoomBookingDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.RoomBookingDetail
{
    public class RoomBookingDetailDeleteViewModel : ViewModelBase<RoomBookingDetailDeleteRequest>
    {
        public readonly IRoomBookingDetailReadWriteRepository _roombookingdetailReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        public RoomBookingDetailDeleteViewModel(IRoomBookingDetailReadWriteRepository roomBookingDetailReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _roombookingdetailReadWriteRepository = roomBookingDetailReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public async override Task HandleAsync(RoomBookingDetailDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roombookingdetailReadWriteRepository.DeleteRoomBookingDetailAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the RoomBookingDetail"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "RoomBookingDetail")
                    }
                };
            }
        }
    }
}
