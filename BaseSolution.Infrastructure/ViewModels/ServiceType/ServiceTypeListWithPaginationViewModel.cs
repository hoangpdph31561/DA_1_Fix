using Azure.Core;
using BaseSolution.Application.DataTransferObjects.ServiceType.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.ServiceType
{

    public class ServiceTypeListWithPaginationViewModel : ViewModelBase<ViewServiceTypeWithPaginationRequest>
    {
        public readonly IServiceTypeReadOnlyRepository _ServiceTypeReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public ServiceTypeListWithPaginationViewModel(IServiceTypeReadOnlyRepository ServiceTypeReadOnlyRepository, ILocalizationService localizationService)
        {
            _ServiceTypeReadOnlyRepository = ServiceTypeReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(ViewServiceTypeWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _ServiceTypeReadOnlyRepository.GetServiceTypeWithPaginationByAdminAsync(request, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the list of ServiceType"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of ServiceType")
                }
            };
            }
        }
    }
}
