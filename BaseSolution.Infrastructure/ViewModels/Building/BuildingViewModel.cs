using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.Building
{
    public class BuildingViewModel : ViewModelBase<Guid>
    {
        private readonly IBuildingReadOnlyRespository _buildingReadOnlyRespository;
        private readonly ILocalizationService _localizationService;
        public BuildingViewModel(IBuildingReadOnlyRespository buildingReadOnlyRespository, ILocalizationService localizationService)
        {
            _buildingReadOnlyRespository = buildingReadOnlyRespository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(Guid idBuilding, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _buildingReadOnlyRespository.GetBuildingByIdAsync(idBuilding, cancellationToken);
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
                        Error = _localizationService["Error occurred while getting the Building"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Building")
                    }
                };
            }
        }
    }
}
