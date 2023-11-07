using AutoMapper;
using BaseSolution.Application.DataTransferObjects.User.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.ViewModels.User
{
    public class UserUpdateViewModel : ViewModelBase<UserUpdateRequest>
    {
        private readonly IUserReadWriteRepository _userReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public UserUpdateViewModel(IUserReadWriteRepository userReadWriteRepository, IMapper mapper, ILocalizationService localizationService)
        {
            _userReadWriteRespository = userReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(UserUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userReadWriteRespository.UpdateUserAsync(_mapper.Map<UserEntity>(request), cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the user"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "user")
                    }
                };
            }
        }
    }
}
