using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.Customer
{
    public class CustomerIdentificationViewModel : ViewModelBase<string>
    {
        public readonly ICustomerReadOnlyRepository _CustomerReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public CustomerIdentificationViewModel(ICustomerReadOnlyRepository CustomerReadOnlyRepository, ILocalizationService localizationService)
        {
            _CustomerReadOnlyRepository = CustomerReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(string identification, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _CustomerReadOnlyRepository.GetCustomerByIdentificationAsync(identification, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the Customer"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Customer")
                }
            };
            }
        }
    }
}
