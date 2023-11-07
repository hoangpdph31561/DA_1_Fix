using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.AmenityRoomDetail
{
    public class AmenityRoomDetailViewModel : ViewModelBase<Guid>
    {
        private readonly IAmenityRoomDetailReadOnlyRepository _AmenityRoomDetailReadOnlyRespository;
        private readonly ILocalizationService _localizationService;
        public AmenityRoomDetailViewModel(IAmenityRoomDetailReadOnlyRepository AmenityRoomDetailReadOnlyRepository, ILocalizationService localizationService)
        {
            _AmenityRoomDetailReadOnlyRespository = AmenityRoomDetailReadOnlyRepository;
            _localizationService = localizationService;
        }

        public async override Task HandleAsync(Guid idAmenityRoomDetail, CancellationToken cancellationToken)
        {

            try
            {
                var result = await _AmenityRoomDetailReadOnlyRespository.GetAmenityRoomDetailByIdAsync(idAmenityRoomDetail, cancellationToken);
                Data = result.Data!;
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
                        Error = _localizationService["Error occurred while getting the AmenityRoomDetail"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "AmenityRoomDetail")
                    }
                };
            }
        }
    }
}
