using AutoMapper;
using Azure.Core;
using BaseSolution.Application.DataTransferObjects.ServiceOrder.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;
using BaseSolution.Infrastructure.Implements.Repositories.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.ServiceOrder
{
    public class ServiceOrderUpdateViewModel : ViewModelBase<ServiceOrderUpdateRequest>
    {
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        private readonly IServiceOrderReadWriteRespository _serviceOrderReadWrite;
        public ServiceOrderUpdateViewModel(IServiceOrderReadWriteRespository serviceOrderReadWrite, IMapper mapper, ILocalizationService localizationService)
        {
            _serviceOrderReadWrite = serviceOrderReadWrite;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(ServiceOrderUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceOrderReadWrite.UpdateServiceOrder(_mapper.Map<ServiceOrderEntity>(request), cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the service order"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "service order")
                    }
                };
            }
        }
    }
}
