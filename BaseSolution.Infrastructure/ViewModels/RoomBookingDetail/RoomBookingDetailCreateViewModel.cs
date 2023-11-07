using AutoMapper;
using BaseSolution.Application.DataTransferObjects.RoomBookingDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.ViewModels.RoomBookingDetail
{
    public class RoomBookingDetailCreateViewModel : ViewModelBase<RoomBookingDetailCreateRequest>
    {
        public readonly IRoomBookingDetailReadOnlyRepository _roombookingdetailReadOnlyRepository;
        public readonly IRoomBookingDetailReadWriteRepository _roombookingdetailReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public RoomBookingDetailCreateViewModel(IRoomBookingDetailReadOnlyRepository roomBookingDetailReadOnlyRepository, IRoomBookingDetailReadWriteRepository roomBookingDetailReadWriteRepository,
            IMapper mapper, ILocalizationService localizationService)
        {
            _roombookingdetailReadOnlyRepository = roomBookingDetailReadOnlyRepository;
            _roombookingdetailReadWriteRespository = roomBookingDetailReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async override Task HandleAsync(RoomBookingDetailCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _roombookingdetailReadWriteRespository.AddRoomBookingDetailAsync(_mapper.Map<RoomBookingDetailEntity>(request), cancellationToken); ;
                if (createResult.Success)
                {
                    var result = await _roombookingdetailReadOnlyRepository.GetRoomBookingDetailByIdAsync(createResult.Data, cancellationToken);

                    Data = result.Data!;
                    Success = result.Success;
                    ErrorItems = result.Errors;
                    Message = result.Message;
                    return;
                }
                Success = createResult.Success;
                ErrorItems = createResult.Errors;
                Message = createResult.Message;
            }
            catch (Exception)
            {
                Success = false;
                ErrorItems = new[]
                {
                    new ErrorItem
                    {
                        Error = _localizationService["Error occurred while getting the RoomBookingDetail"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "RoomBookingDetail")
                    }
                };
            }
        }
    }
}
