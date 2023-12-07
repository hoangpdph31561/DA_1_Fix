using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.Roombooking
{
    public class RoomBookingByCustomerIdViewModel : ViewModelBase<Guid>
    {
        private readonly IRoombookingReadOnlyRepository _roombookingReadOnlyRespository;
        private readonly ILocalizationService _localizationService;
        public RoomBookingByCustomerIdViewModel(IRoombookingReadOnlyRepository roombookingReadOnlyRepository, ILocalizationService localizationService)
        {
            _roombookingReadOnlyRespository = roombookingReadOnlyRepository;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(Guid idCustomer, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roombookingReadOnlyRespository.GetRoombookingByIdCustomerAsync(idCustomer, cancellationToken);
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
                        Error = _localizationService["Error occurred while getting the roombooking"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "roombooking")
                    }
                };
            }
        }
    }
}
