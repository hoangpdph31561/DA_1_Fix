using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Example.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.ViewModels.Example
{
    public class ExampleCreateViewModel : ViewModelBase<ExampleCreateRequest>
    {
        public readonly IExampleReadOnlyRepository _exampleReadOnlyRepository;
        public readonly IExampleReadWriteRepository _exampleReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public ExampleCreateViewModel(IExampleReadOnlyRepository ExampleReadOnlyRepository, IExampleReadWriteRepository ExampleReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _exampleReadOnlyRepository = ExampleReadOnlyRepository;
            _exampleReadWriteRepository = ExampleReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public override async Task HandleAsync(ExampleCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _exampleReadWriteRepository.AddExampleAsync(_mapper.Map<ExampleEntity>(request), cancellationToken);

                if (createResult.Success)
                {
                    var result = await _exampleReadOnlyRepository.GetExampleByIdAsync(createResult.Data, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the Example"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Example")
                    }
                };
            }
        }
    }
}
