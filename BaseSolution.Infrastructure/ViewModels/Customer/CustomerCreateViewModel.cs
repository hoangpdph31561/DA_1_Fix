using AutoMapper;
using Azure.Core;
using BaseSolution.Application.DataTransferObjects.Customer.Request;
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

namespace BaseSolution.Infrastructure.ViewModels.Customer
{
    public class CustomerCreateViewModel : ViewModelBase<CustomerCreateRequest>
    {
        public readonly ICustomerReadOnlyRepository _CustomerReadOnlyRepository;
        public readonly ICustomerReadWriteRepository _CustomerReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public CustomerCreateViewModel(ICustomerReadOnlyRepository CustomerReadOnlyRepository, ICustomerReadWriteRepository CustomerReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _CustomerReadOnlyRepository = CustomerReadOnlyRepository;
            _CustomerReadWriteRepository = CustomerReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(CustomerCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _CustomerReadWriteRepository.AddCustomerAsync(_mapper.Map<CustomerEntity>(request), cancellationToken);

                if (createResult.Success)
                {
                    var result = await _CustomerReadOnlyRepository.GetCustomerByIdAsync(createResult.Data, cancellationToken);

                    Data = createResult;
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
                        Error = _localizationService["Error occurred while getting the Customer"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Customer")
                    }
                };
            }
        }
    }
}
