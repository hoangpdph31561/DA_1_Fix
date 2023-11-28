using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.User
{
    public class UserNameViewModel : ViewModelBase<string>
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRespository;
        private readonly ILocalizationService _localizationService;
        public UserNameViewModel(IUserReadOnlyRepository userReadOnlyRepository, ILocalizationService localizationService)
        {
            _userReadOnlyRespository = userReadOnlyRepository;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(string userName, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userReadOnlyRespository.GetUserByUserNameAsync(userName, cancellationToken);
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
                        Error = _localizationService["Error occurred while getting the user"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "user")
                    }
                };
            }
        }
    }
}
