using AutoMapper;
using Azure.Core;
using BaseSolution.Application.DataTransferObjects.Services.Request;
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

namespace BaseSolution.Infrastructure.ViewModels.Service
{
    public class ServiceCreateViewModel : ViewModelBase<ServiceCreateRequest>
    {
        public readonly IServiceReadOnlyRepository _ServiceReadOnlyRepository;
        public readonly IServicesReadWriteRepository _ServiceReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public ServiceCreateViewModel(IServiceReadOnlyRepository ServiceReadOnlyRepository, IServicesReadWriteRepository ServiceReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _ServiceReadOnlyRepository = ServiceReadOnlyRepository;
            _ServiceReadWriteRepository = ServiceReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(ServiceCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _ServiceReadWriteRepository.AddServiceAsync(_mapper.Map<ServiceEntity>(request), cancellationToken);

                if (createResult.Success)
                {
                    var result = await _ServiceReadOnlyRepository.GetServiceByIdAsync(createResult.Data, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the Service"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Service")
                    }
                };
            }
        }
    }
}
