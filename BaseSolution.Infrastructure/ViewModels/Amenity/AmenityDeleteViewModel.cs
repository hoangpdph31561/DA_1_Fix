using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Amenity.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.Amenity
{
    public class AmenityDeleteViewModel : ViewModelBase<AmenityDeleteRequest>
    {
        public readonly IAmenityReadWriteRepository _amenityReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        public AmenityDeleteViewModel(IAmenityReadWriteRepository amenityReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _amenityReadWriteRepository = amenityReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public async override Task HandleAsync(AmenityDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _amenityReadWriteRepository.DeleteAmenityAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the Amenity"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "Amenity")
                    }
                };
            }
        }
    }
}
