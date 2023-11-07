using AutoMapper;
using BaseSolution.Application.DataTransferObjects.User.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.ViewModels.User
{
    public class UserCreateViewModel : ViewModelBase<UserCreateRequest>
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUserReadWriteRepository _userReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public UserCreateViewModel(IUserReadOnlyRepository userReadOnlyRepository, IUserReadWriteRepository userReadWriteRepository,
            IMapper mapper, ILocalizationService localizationService)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _userReadWriteRespository = userReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(UserCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _userReadWriteRespository.AddUserAsync(_mapper.Map<UserEntity>(request), cancellationToken);
                if (createResult.Success)
                {
                    var result = await _userReadOnlyRepository.GetUserByIdAsync(createResult.Data, cancellationToken);

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
