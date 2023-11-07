using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Amenity.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.ViewModels.Amenity
{
    public class AmenityCreateViewModel : ViewModelBase<AmenityCreateRequest>
    {
        public readonly IAmenityReadOnlyRepository _amenityReadOnlyRepository;
        public readonly IAmenityReadWriteRepository _amenityReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public AmenityCreateViewModel(IAmenityReadOnlyRepository amenityReadOnlyRepository, IAmenityReadWriteRepository amenityReadWriteRepository, IMapper mapper, ILocalizationService localizationService)
        {
            _amenityReadOnlyRepository = amenityReadOnlyRepository;
            _amenityReadWriteRespository = amenityReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async override Task HandleAsync(AmenityCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _amenityReadWriteRespository.AddAmenityAsync(_mapper.Map<AmenityEntity>(request), cancellationToken);
                if (createResult.Success)
                {
                    var result = await _amenityReadOnlyRepository.GetAmenityByIdAsync(createResult.Data, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the Amenity"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Amenity")
                    }
                };
            }
        }
    }
}
