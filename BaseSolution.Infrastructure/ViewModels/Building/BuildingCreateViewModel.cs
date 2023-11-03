using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Building.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;
using BaseSolution.Infrastructure.Implements.Repositories.ReadOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.Building
{
    public class BuildingCreateViewModel : ViewModelBase<BuildingCreateRequest>
    {
        private readonly IBuildingReadOnlyRespository _buildingReadOnlyRespository;
        private readonly IBuildingReadWriteRespository _buildingReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public BuildingCreateViewModel(IBuildingReadOnlyRespository buildingReadOnlyRespository, IBuildingReadWriteRespository buildingReadWriteRespository, IMapper mapper, ILocalizationService localizationService)
        {
            _buildingReadOnlyRespository = buildingReadOnlyRespository;
            _buildingReadWriteRespository = buildingReadWriteRespository;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(BuildingCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _buildingReadWriteRespository.AddBuildingAsync(_mapper.Map<BuildingEntity>(request), cancellationToken);
                if (createResult.Success)
                {
                    var result = await _buildingReadOnlyRespository.GetBuildingByIdAsync(createResult.Data, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the building"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "building")
                    }
                };
            }
        }
    }
}
