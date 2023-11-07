﻿using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Infrastructure.Implements.Repositories.ReadOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.ServiceOrderDetail
{
    public class ServiceOrderDetailViewModel : ViewModelBase<Guid>
    {
        private readonly IServiceOrderDetailReadOnlyRespository _serviceOrderDetailReadOnly;
        private readonly ILocalizationService _localizationService;
        public ServiceOrderDetailViewModel(IServiceOrderDetailReadOnlyRespository serviceOrderDetailReadOnly, ILocalizationService localizationService)
        {
            _localizationService = localizationService;
            _serviceOrderDetailReadOnly = serviceOrderDetailReadOnly;
        }
        public async override Task HandleAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceOrderDetailReadOnly.GetServiceOrderDetailByIdAsync(id, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the service order detail"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "service order detail")
                }
            };
            }
        }
    }
}
