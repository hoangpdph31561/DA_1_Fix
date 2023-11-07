using AutoMapper;
using Azure.Core;
using BaseSolution.Application.DataTransferObjects.ServiceType.Request;
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
    public class ServiceTypeUpdateViewModel : ViewModelBase<ServiceTypeUpDateRequest>
    {
        public readonly IServiceTypeReadWriteRepository _ServiceTypeReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public ServiceTypeUpdateViewModel(IServiceTypeReadWriteRepository ServiceTypeReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _ServiceTypeReadWriteRepository = ServiceTypeReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(ServiceTypeUpDateRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var result = await _ServiceTypeReadWriteRepository.UpdateServiceTypeAsync(_mapper.Map<ServiceTypeEntity>(request), cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the ServiceType"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "ServiceType")
                    }
                };
            }
        }
    }
}
