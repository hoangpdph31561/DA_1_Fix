using Azure.Core;
using BaseSolution.Application.DataTransferObjects.Building.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Infrastructure.Implements.Repositories.ReadOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.Building
{
    public class BuildingListWithPaginationByAdminViewModel : ViewModelBase<ViewBuildingWithPaginationRequest>
    {
        private readonly IBuildingReadOnlyRespository _buildingReadOnlyRespository;
        private readonly ILocalizationService _localizationService;
        public BuildingListWithPaginationByAdminViewModel(IBuildingReadOnlyRespository buildingReadOnlyRespository, ILocalizationService localizationService)
        {
            _buildingReadOnlyRespository = buildingReadOnlyRespository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(ViewBuildingWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _buildingReadOnlyRespository.GetBuildingWithPaginationByAdminAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the list of Building"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of Building")
                    }
                };
            }
        }
    }
}
