using AutoMapper;
using BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.ViewModels.AmenityRoomDetail
{
    public class AmenityRoomDetailCreateViewModel : ViewModelBase<AmenityRoomDetailCreateRequest>
    {
        public readonly IAmenityRoomDetailReadOnlyRepository _AmenityRoomDetailReadOnlyRepository;
        public readonly IAmenityRoomDetailReadWriteRepository _AmenityRoomDetailReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public AmenityRoomDetailCreateViewModel(IAmenityRoomDetailReadOnlyRepository AmenityRoomDetailReadOnlyRepository, IAmenityRoomDetailReadWriteRepository AmenityRoomDetailReadWriteRepository, IMapper mapper, ILocalizationService localizationService)
        {
            _AmenityRoomDetailReadOnlyRepository = AmenityRoomDetailReadOnlyRepository;
            _AmenityRoomDetailReadWriteRespository = AmenityRoomDetailReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async override Task HandleAsync(AmenityRoomDetailCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _AmenityRoomDetailReadWriteRespository.AddAmenityRoomDetailAsync(_mapper.Map<AmenityRoomDetailEntity>(request), cancellationToken);
                if (createResult.Success)
                {
                    var result = await _AmenityRoomDetailReadOnlyRepository.GetAmenityRoomDetailByIdAsync(createResult.Data, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the AmenityRoomDetail"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "AmenityRoomDetail")
                    }
                };
            }
        }
    }
}
