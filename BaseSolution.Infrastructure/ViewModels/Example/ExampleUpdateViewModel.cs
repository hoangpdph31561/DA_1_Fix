using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Example.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.ViewModels.Example
{
    public class ExampleUpdateViewModel : ViewModelBase<ExampleUpdateRequest>
    {
        public readonly IExampleReadWriteRepository _exampleReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public ExampleUpdateViewModel(IExampleReadWriteRepository ExampleReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _exampleReadWriteRepository = ExampleReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public override async Task HandleAsync(ExampleUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _exampleReadWriteRepository.UpdateExampleAsync(_mapper.Map<ExampleEntity>(request), cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the Example"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "Example")
                    }
                };
            }
        }
    }
}
