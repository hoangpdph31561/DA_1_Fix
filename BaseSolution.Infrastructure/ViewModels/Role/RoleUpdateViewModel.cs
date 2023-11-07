using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Role.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.ViewModels.Role
{
    public class RoleUpdateViewModel : ViewModelBase<RoleUpdateRequest>
    {
        private readonly IRoleReadWriteRepository _roleReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public RoleUpdateViewModel(IRoleReadWriteRepository roleReadWriteRepository, IMapper mapper, ILocalizationService localizationService)
        {
            _roleReadWriteRespository = roleReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(RoleUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roleReadWriteRespository.UpdateRoleAsync(_mapper.Map<UserRoleEntity>(request), cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the role"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "role")
                    }
                };
            }
        }
    }
}
