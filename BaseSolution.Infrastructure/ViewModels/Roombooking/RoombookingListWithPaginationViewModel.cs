using BaseSolution.Application.DataTransferObjects.Roombooking.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.Roombooking
{
    public class RoombookingListWithPaginationViewModel : ViewModelBase<ViewRoombookingWithPaginationRequest>
    {
        private readonly IRoombookingReadOnlyRepository _roombookingReadOnlyRepository;
        private readonly ILocalizationService _localizationService;
        public RoombookingListWithPaginationViewModel(IRoombookingReadOnlyRepository roombookingReadOnlyRepository, ILocalizationService localizationService)
        {
            _roombookingReadOnlyRepository = roombookingReadOnlyRepository;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(ViewRoombookingWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roombookingReadOnlyRepository.GetRoombookingWithPaginationByAdminAsync(request, cancellationToken);

                Data = result.Data!;
                Success = result.Success;
                ErrorItems = result.Errors;
                Message = result.Message;
                return;
            }
            catch
            {

                Success = false;
                ErrorItems = new[]
                {
                    new ErrorItem
                    {
                        Error = _localizationService["Error occurred while getting the list of roombooking"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of roombooking")
                    }
                };
            }
        }
    }
}
