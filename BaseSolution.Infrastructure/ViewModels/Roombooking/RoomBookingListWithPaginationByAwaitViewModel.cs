using BaseSolution.Application.DataTransferObjects.Roombooking.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.Roombooking
{
    public class RoomBookingListWithPaginationByAwaitViewModel : ViewModelBase<ViewRoombookingWithPaginationRequest>
    {

        private readonly IRoombookingReadOnlyRepository _roombookingReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public RoomBookingListWithPaginationByAwaitViewModel(IRoombookingReadOnlyRepository roombookingReadOnlyRepository, ILocalizationService localizationService)
        {
            _roombookingReadOnlyRepository = roombookingReadOnlyRepository;
            _localizationService = localizationService;
        }

        public override async Task HandleAsync(ViewRoombookingWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roombookingReadOnlyRepository.GetRoombookingWithPaginationByAwaitAsync(request, cancellationToken);

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
