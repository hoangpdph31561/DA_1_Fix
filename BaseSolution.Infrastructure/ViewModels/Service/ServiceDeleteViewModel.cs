using AutoMapper;
using Azure.Core;
using BaseSolution.Application.DataTransferObjects.Services.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
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
    public class ServiceDeleteViewModel : ViewModelBase<ServiceDeleteRequest>
    {

        public readonly IServicesReadWriteRepository _ServiceReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public ServiceDeleteViewModel(IServicesReadWriteRepository ServiceReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _ServiceReadWriteRepository = ServiceReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public override async Task HandleAsync(ServiceDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _ServiceReadWriteRepository.DeleteServiceAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the Service"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "Service")
                    }
                };
            }
        }
    }
}
