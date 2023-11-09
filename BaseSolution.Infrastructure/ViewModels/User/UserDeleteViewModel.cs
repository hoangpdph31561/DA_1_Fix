using AutoMapper;
using BaseSolution.Application.DataTransferObjects.User.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.User
{
    public class UserDeleteViewModel : ViewModelBase<UserDeleteRequest>
    {
        public readonly IUserReadWriteRepository _UserReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        public UserDeleteViewModel(IUserReadWriteRepository UserReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _UserReadWriteRepository = UserReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public async override Task HandleAsync(UserDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _UserReadWriteRepository.DeleteUserAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the User"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "User")
                    }
                };
            }
        }
    }
}
