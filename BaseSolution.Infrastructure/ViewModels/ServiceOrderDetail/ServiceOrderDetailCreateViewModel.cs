using AutoMapper;
using Azure.Core;
using BaseSolution.Application.DataTransferObjects.ServiceOrderDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;
using BaseSolution.Infrastructure.Implements.Repositories.ReadOnly;
using BaseSolution.Infrastructure.Implements.Repositories.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.ServiceOrderDetail
{
    public class ServiceOrderDetailCreateViewModel : ViewModelBase<ServiceOrderDetailCreateRequest>
    {
        private readonly IServiceOrderDetailReadOnlyRespository _serviceOrderDetailReadOnly;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        private readonly IServiceOrderDetailReadWriteRespository _serviceOrderDetailReadWrite;
        public ServiceOrderDetailCreateViewModel(IServiceOrderDetailReadOnlyRespository serviceOrderDetailReadOnly, IServiceOrderDetailReadWriteRespository serviceOrderDetailReadWrite, IMapper mapper, ILocalizationService localizationService)
        {
            _localizationService = localizationService;
            _mapper = mapper;
            _serviceOrderDetailReadOnly = serviceOrderDetailReadOnly;
            _serviceOrderDetailReadWrite = serviceOrderDetailReadWrite;
        }
        public async override Task HandleAsync(ServiceOrderDetailCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _serviceOrderDetailReadWrite.CreateNewServiceOrderDetail(_mapper.Map<ServiceOrderDetailEntity>(request), cancellationToken);

                if (createResult.Success)
                {
                    var result = await _serviceOrderDetailReadOnly.GetServiceOrderDetailByIdAsync(createResult.Data, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the service order detail"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "service order detail")
                    }
                };
            }
        }
    }
}
