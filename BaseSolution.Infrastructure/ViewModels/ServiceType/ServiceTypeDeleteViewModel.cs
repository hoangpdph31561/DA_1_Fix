using AutoMapper;
using Azure.Core;
using BaseSolution.Application.DataTransferObjects.ServiceType.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
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
    public class ServiceTypeDeleteViewModel : ViewModelBase<ServiceTypeDeleteRequest>
    {

        public readonly IServiceTypeReadWriteRepository _ServiceTypeReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public ServiceTypeDeleteViewModel(IServiceTypeReadWriteRepository ServiceTypeReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _ServiceTypeReadWriteRepository = ServiceTypeReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(ServiceTypeDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _ServiceTypeReadWriteRepository.DeleteServiceTypeAsync(request, cancellationToken);

                Success = result.Success;
                ErrorItems = result.Errors;
                Message = result.Message;
                return;
            }
            catch (Exception)
            {
                Success = false;
                ErrorItems = new[]
                    {
                    new ErrorItem
                    {
                        Error = _localizationService["Error occurred while updating the ServiceType"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "ServiceType")
                    }
                };
            }
        }
    }
}
