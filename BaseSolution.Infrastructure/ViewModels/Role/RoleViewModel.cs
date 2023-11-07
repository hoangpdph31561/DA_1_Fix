using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.Role;

public class RoleViewModel : ViewModelBase<Guid>
{
    private readonly IRoleReadOnlyRepository _roleReadOnlyRespository;
    private readonly ILocalizationService _localizationService;
    public RoleViewModel(IRoleReadOnlyRepository roleReadOnlyRepository, ILocalizationService localizationService)
    {
        _roleReadOnlyRespository = roleReadOnlyRepository;
        _localizationService = localizationService;
    }

    public async override Task HandleAsync(Guid idRole, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _roleReadOnlyRespository.GetRoleByIdAsync(idRole, cancellationToken);
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
                    Error = _localizationService["Error occurred while getting the role"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "role")
                }
            };
        }
    }
}
