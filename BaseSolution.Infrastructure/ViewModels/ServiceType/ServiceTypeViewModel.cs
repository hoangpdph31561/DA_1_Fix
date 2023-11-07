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
    public class ServiceTypeViewModel : ViewModelBase<Guid>
    {
        public readonly IServiceTypeReadOnlyRepository _ServiceTypeReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public ServiceTypeViewModel(IServiceTypeReadOnlyRepository ServiceTypeReadOnlyRepository, ILocalizationService localizationService)
        {
            _ServiceTypeReadOnlyRepository = ServiceTypeReadOnlyRepository;
            _localizationService = localizationService;
        }

        public override async Task HandleAsync(Guid idServiceType, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _ServiceTypeReadOnlyRepository.GetServiceTypeByIdAsync(idServiceType, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the ServiceType"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "ServiceType")
                }
            };
            }
        }
    }
}
