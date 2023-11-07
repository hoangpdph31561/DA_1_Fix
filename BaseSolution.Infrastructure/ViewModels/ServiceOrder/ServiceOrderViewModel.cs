using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Infrastructure.Implements.Repositories.ReadOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.ServiceOrder
{
    public class ServiceOrderViewModel : ViewModelBase<Guid>
    {
        private readonly IServiceOrderReadOnlyRespository _serviceOrderReadOnly;
        private readonly ILocalizationService _localizationService;
        public ServiceOrderViewModel(IServiceOrderReadOnlyRespository serviceOrderReadOnly, ILocalizationService localizationService)
        {
            _localizationService = localizationService;
            _serviceOrderReadOnly = serviceOrderReadOnly;
        }
        public async override Task HandleAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceOrderReadOnly.GetServiceOrderByIdAsync(id, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the Service order"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Service order")
                    }
                };
            }
        }
    }
}
