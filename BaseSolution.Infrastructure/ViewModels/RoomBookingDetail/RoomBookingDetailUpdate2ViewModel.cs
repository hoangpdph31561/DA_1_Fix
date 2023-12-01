using AutoMapper;
using BaseSolution.Application.DataTransferObjects.RoomBookingDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.RoomBookingDetail
{
    public class RoomBookingDetailUpdate2ViewModel : ViewModelBase<RoomBookingDetailUpdate2Request>
    {
        private readonly IRoomBookingDetailReadWriteRepository _roombookingdetailReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public RoomBookingDetailUpdate2ViewModel(IRoomBookingDetailReadWriteRepository roomBookingDetailReadWriteRepository, IMapper mapper, ILocalizationService localizationService)
        {
            _roombookingdetailReadWriteRespository = roomBookingDetailReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public override async Task HandleAsync(RoomBookingDetailUpdate2Request request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roombookingdetailReadWriteRespository.UpdateRoomBookingDetail2Async(request, cancellationToken);

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
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "RoomBookingDetail")
                    }
                };
            }
        }
    }
}
