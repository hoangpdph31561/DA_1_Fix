using AutoMapper;
using BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.AmenityRoomDetail
{
    public class AmenityRoomDetailDeleteViewModel : ViewModelBase<AmenityRoomDetailDeleteRequest>
    {
        public readonly IAmenityRoomDetailReadWriteRepository _AmenityRoomDetailReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        public AmenityRoomDetailDeleteViewModel(IAmenityRoomDetailReadWriteRepository AmenityRoomDetailReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _AmenityRoomDetailReadWriteRepository = AmenityRoomDetailReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public async override Task HandleAsync(AmenityRoomDetailDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _AmenityRoomDetailReadWriteRepository.DeleteAmenityRoomDetailAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the AmenityRoomDetail"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "AmenityRoomDetail")
                    }
                };
            }
        }
    }
}
