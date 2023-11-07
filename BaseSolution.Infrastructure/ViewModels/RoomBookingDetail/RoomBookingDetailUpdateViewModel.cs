using AutoMapper;
using BaseSolution.Application.DataTransferObjects.RoomBookingDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.ViewModels.RoomBookingDetail
{
    public class RoomBookingDetailUpdateViewModel : ViewModelBase<RoomBookingDetailUpdateRequest>
    {
        private readonly IRoomBookingDetailReadWriteRepository _roombookingdetailReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public RoomBookingDetailUpdateViewModel(IRoomBookingDetailReadWriteRepository roomBookingDetailReadWriteRepository, IMapper mapper, ILocalizationService localizationService)
        {
            _roombookingdetailReadWriteRespository = roomBookingDetailReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async override Task HandleAsync(RoomBookingDetailUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roombookingdetailReadWriteRespository.UpdateRoomBookingDetailAsync(_mapper.Map<RoomBookingDetailEntity>(request), cancellationToken);

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
