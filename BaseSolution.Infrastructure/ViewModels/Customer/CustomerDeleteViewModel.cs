using AutoMapper;
using Azure.Core;
using BaseSolution.Application.DataTransferObjects.Customer.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.Customer
{
    public class CustomerDeleteViewModel : ViewModelBase<CustomerDeleteRequest>
    {

        public readonly ICustomerReadWriteRepository _CustomerReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public CustomerDeleteViewModel(ICustomerReadWriteRepository CustomerReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _CustomerReadWriteRepository = CustomerReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(CustomerDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _CustomerReadWriteRepository.DeleteCustomerAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the Customer"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "Customer")
                    }
                };
            }
        }
    }
}
