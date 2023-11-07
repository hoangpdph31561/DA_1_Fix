using AutoMapper;
using Azure.Core;
using BaseSolution.Application.DataTransferObjects.ServiceOrderDetail.Request;
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

namespace BaseSolution.Infrastructure.ViewModels.ServiceOrderDetail
{
    public class ServiceOrderDetailUpdateViewModel : ViewModelBase<ServiceOrderDetailUpdateRequest>
    {
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        private readonly IServiceOrderDetailReadWriteRespository _serviceOrderDetailReadWrite;
        public ServiceOrderDetailUpdateViewModel(IServiceOrderDetailReadWriteRespository serviceOrderDetailReadWrite, IMapper mapper, ILocalizationService localizationService)
        {
            _serviceOrderDetailReadWrite = serviceOrderDetailReadWrite;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(ServiceOrderDetailUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceOrderDetailReadWrite.UpdateServiceOrderDetail(_mapper.Map<ServiceOrderDetailEntity>(request), cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the service order detail"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "service order detail")
                    }
                };
            }
        }
    }
}
