using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Customer.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.ViewModels.Customer
{
    public class CustomerDetailUpdateViewModel : ViewModelBase<CustomerDetailUpdateRequest>
    {
        public readonly ICustomerReadWriteRepository _CustomerReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public CustomerDetailUpdateViewModel(ICustomerReadWriteRepository CustomerReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _CustomerReadWriteRepository = CustomerReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(CustomerDetailUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _CustomerReadWriteRepository.UpdateCustomerDetailAsync(_mapper.Map<CustomerEntity>(request), cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the Customer"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "Customer")
                    }
                };
            }
        }
    }
}
