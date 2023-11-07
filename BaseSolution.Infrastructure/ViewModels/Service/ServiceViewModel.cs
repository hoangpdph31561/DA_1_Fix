using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.Service
{
    public class ServiceViewModel : ViewModelBase<Guid>
    {
        public readonly IServiceReadOnlyRepository _ServiceReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public ServiceViewModel(IServiceReadOnlyRepository ServiceReadOnlyRepository, ILocalizationService localizationService)
        {
            _ServiceReadOnlyRepository = ServiceReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(Guid idService, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _ServiceReadOnlyRepository.GetServiceByIdAsync(idService, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the Service"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Service")
                }
            };
            }
        }
    }
}
