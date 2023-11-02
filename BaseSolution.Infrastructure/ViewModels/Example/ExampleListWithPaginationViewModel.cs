using BaseSolution.Application.DataTransferObjects.Example.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.Example
{
    public class ExampleListWithPaginationViewModel : ViewModelBase<ViewExampleWithPaginationRequest>
    {
        public readonly IExampleReadOnlyRepository _exampleReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public ExampleListWithPaginationViewModel(IExampleReadOnlyRepository ExampleReadOnlyRepository, ILocalizationService localizationService)
        {
            _exampleReadOnlyRepository = ExampleReadOnlyRepository;
            _localizationService = localizationService;
        }

        public override async Task HandleAsync(ViewExampleWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _exampleReadOnlyRepository.GetExampleWithPaginationByAdminAsync(request, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the list of Example"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of Example")
                }
            };
            }
        }
    }
}
