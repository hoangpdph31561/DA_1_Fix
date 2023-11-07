using AutoMapper;
using BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.ViewModels.AmenityRoomDetail
{
    public class AmenityRoomDetailUpdateViewModel : ViewModelBase<AmenityRoomDetailUpdateRequest>
    {
        private readonly IAmenityRoomDetailReadWriteRepository _AmenityRoomDetailReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public AmenityRoomDetailUpdateViewModel(IAmenityRoomDetailReadWriteRepository AmenityRoomDetailReadWriteRepository, IMapper mapper, ILocalizationService localizationService)
        {
            _AmenityRoomDetailReadWriteRespository = AmenityRoomDetailReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async override Task HandleAsync(AmenityRoomDetailUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _AmenityRoomDetailReadWriteRespository.UpdateAmenityRoomDetailAsync(_mapper.Map<AmenityRoomDetailEntity>(request), cancellationToken);

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
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "AmenityRoomDetail")
                    }
                };
            }
        }
    }
}
