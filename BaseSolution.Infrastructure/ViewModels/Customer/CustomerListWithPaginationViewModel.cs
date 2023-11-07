using Azure.Core;
using BaseSolution.Application.DataTransferObjects.Customer.Request;
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
    public class CustomerListWithPaginationViewModel : ViewModelBase<ViewCustomerWithPaginationRequest>
    {
        public readonly ICustomerReadOnlyRepository _CustomerReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public CustomerListWithPaginationViewModel(ICustomerReadOnlyRepository CustomerReadOnlyRepository, ILocalizationService localizationService)
        {
            _CustomerReadOnlyRepository = CustomerReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(ViewCustomerWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _CustomerReadOnlyRepository.GetCustomerWithPaginationByAdminAsync(request, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the list of Customer"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of Customer")
                }
            };
            }
        }
    }
}
