using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Amenity.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.ViewModels.Amenity
{
    public class AmenityUpdateViewModel : ViewModelBase<AmenityUpdateRequest>
    {
        private readonly IAmenityReadWriteRepository _amenityReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public AmenityUpdateViewModel(IAmenityReadWriteRepository amenityReadWriteRepository, IMapper mapper, ILocalizationService localizationService)
        {
            _amenityReadWriteRespository = amenityReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async override Task HandleAsync(AmenityUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _amenityReadWriteRespository.UpdateAmenityAsync(_mapper.Map<AmenityEntity>(request), cancellationToken);

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
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "Amenity")
                    }
                };
            }
        }
    }
}
