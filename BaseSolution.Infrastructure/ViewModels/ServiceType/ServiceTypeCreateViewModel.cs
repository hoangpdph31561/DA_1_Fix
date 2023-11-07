using AutoMapper;
using Azure.Core;
using BaseSolution.Application.DataTransferObjects.ServiceType.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.ServiceType
{
    public class ServiceTypeCreateViewModel : ViewModelBase<ServiceTypeCreateRequest>
    {
        public readonly IServiceTypeReadOnlyRepository _ServiceTypeReadOnlyRepository;
        public readonly IServiceTypeReadWriteRepository _ServiceTypeReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public ServiceTypeCreateViewModel(IServiceTypeReadOnlyRepository ServiceTypeReadOnlyRepository, IServiceTypeReadWriteRepository ServiceTypeReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _ServiceTypeReadOnlyRepository = ServiceTypeReadOnlyRepository;
            _ServiceTypeReadWriteRepository = ServiceTypeReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(ServiceTypeCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _ServiceTypeReadWriteRepository.AddServiceTypeAsync(_mapper.Map<ServiceTypeEntity>(request), cancellationToken);

                if (createResult.Success)
                {
                    var result = await _ServiceTypeReadOnlyRepository.GetServiceTypeByIdAsync(createResult.Data, cancellationToken);

                    Data = result.Data!;
                    Success = result.Success;
                    ErrorItems = result.Errors;
                    Message = result.Message;
                    return;
                }

                Success = createResult.Success;
                ErrorItems = createResult.Errors;
                Message = createResult.Message;
            }
            catch (Exception)
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
