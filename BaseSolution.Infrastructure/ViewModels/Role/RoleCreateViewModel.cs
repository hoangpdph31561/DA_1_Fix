using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Role.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.ViewModels.Role
{
    public class RoleCreateViewModel : ViewModelBase<RoleCreateRequest>
    {
        public readonly IRoleReadOnlyRepository _roleReadOnlyRepository;
        public readonly IRoleReadWriteRepository _roleReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public RoleCreateViewModel(IRoleReadOnlyRepository roleReadOnlyRepository, IRoleReadWriteRepository roleReadWriteRepository, IMapper mapper, ILocalizationService localizationService)
        {
            _roleReadOnlyRepository = roleReadOnlyRepository;
            _roleReadWriteRespository = roleReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async override Task HandleAsync(RoleCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _roleReadWriteRespository.AddRoleAsync(_mapper.Map<UserRoleEntity>(request), cancellationToken);
                if (createResult.Success)
                {
                    var result = await _roleReadOnlyRepository.GetRoleByIdAsync(createResult.Data, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the Role"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Role")
                    }
                };
            }
        }
    }
}
