using BaseSolution.Application.DataTransferObjects.Amenity.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.Amenity
{
    public class AmenityListWithPaginationViewModel : ViewModelBase<ViewAmenityWithPaginationRequest>
    {
        private readonly IAmenityReadOnlyRepository _amenityReadOnlyRepository;
        private readonly ILocalizationService _localizationService;
        public AmenityListWithPaginationViewModel(IAmenityReadOnlyRepository amenityReadOnlyRepository, ILocalizationService localizationService)
        {
            _amenityReadOnlyRepository = amenityReadOnlyRepository;
            _localizationService = localizationService;
        }

        public async override Task HandleAsync(ViewAmenityWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _amenityReadOnlyRepository.GetAmenityWithPaginationByAdminAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the list of Amenity"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of Amenity")
                    }
                };
            }
        }
    }
}
