using BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.AmenityRoomDetail
{
    public class AmenityRoomDetailListWithPaginationViewModel : ViewModelBase<ViewAmenityRoomDetailWithPaginationRequest>
    {
        private readonly IAmenityRoomDetailReadOnlyRepository _AmenityRoomDetailReadOnlyRepository;
        private readonly ILocalizationService _localizationService;
        public AmenityRoomDetailListWithPaginationViewModel(IAmenityRoomDetailReadOnlyRepository AmenityRoomDetailReadOnlyRepository, ILocalizationService localizationService)
        {
            _AmenityRoomDetailReadOnlyRepository = AmenityRoomDetailReadOnlyRepository;
            _localizationService = localizationService;
        }

        public async override Task HandleAsync(ViewAmenityRoomDetailWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _AmenityRoomDetailReadOnlyRepository.GetAmenityRoomDetailWithPaginationByAdminAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the list of AmenityRoomDetail"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of AmenityRoomDetail")
                    }
                };
            }
        }
    }
}
