using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Example.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.Example
{
    public class ExampleDeleteViewModel : ViewModelBase<ExampleDeleteRequest>
    {
        public readonly IExampleReadWriteRepository _exampleReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public ExampleDeleteViewModel(IExampleReadWriteRepository ExampleReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _exampleReadWriteRepository = ExampleReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public override async Task HandleAsync(ExampleDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _exampleReadWriteRepository.DeleteExampleAsync(request, cancellationToken);

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
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "Example")
                    }
                };
            }
        }
    }
}
