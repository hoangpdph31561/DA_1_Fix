using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.Amenity
{
    public class AmenityViewModel : ViewModelBase<Guid>
    {
        private readonly IAmenityReadOnlyRepository _amenityReadOnlyRespository;
        private readonly ILocalizationService _localizationService;
        public AmenityViewModel(IAmenityReadOnlyRepository amenityReadOnlyRepository, ILocalizationService localizationService)
        {
            _amenityReadOnlyRespository = amenityReadOnlyRepository;
            _localizationService = localizationService;
        }

        public async override Task HandleAsync(Guid idAmenity, CancellationToken cancellationToken)
        {

            try
            {
                var result = await _amenityReadOnlyRespository.GetAmenityByIdAsync(idAmenity, cancellationToken);
                Data = result.Data!;
                Success = result.Success;
                ErrorItems = result.Errors;
                Message = result.Message;
                return;
            }
            catch(Exception)
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
