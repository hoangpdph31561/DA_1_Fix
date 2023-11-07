using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.Customer
{
    public class CustomerViewModel : ViewModelBase<Guid>
    {
        public readonly ICustomerReadOnlyRepository _CustomerReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public CustomerViewModel(ICustomerReadOnlyRepository CustomerReadOnlyRepository, ILocalizationService localizationService)
        {
            _CustomerReadOnlyRepository = CustomerReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(Guid idCustomer, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _CustomerReadOnlyRepository.GetCustomerByIdAsync(idCustomer, cancellationToken);

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
