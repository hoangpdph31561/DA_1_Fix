using AutoMapper;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.Customer
{
    public class CustomerUpdateStatusViewModel : ViewModelBase<Guid>
    {
        public readonly ICustomerReadWriteRepository _CustomerReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public CustomerUpdateStatusViewModel(ICustomerReadWriteRepository CustomerReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _CustomerReadWriteRepository = CustomerReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _CustomerReadWriteRepository.UpdateStatusCustomerAsync(id, cancellationToken);

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
