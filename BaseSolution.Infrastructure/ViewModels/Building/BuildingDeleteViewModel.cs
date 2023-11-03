using AutoMapper;
using Azure.Core;
using BaseSolution.Application.DataTransferObjects.Building.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Infrastructure.Implements.Repositories.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.Building
{
    public class BuildingDeleteViewModel : ViewModelBase<BuildingDeleteRequest>
    {
        private readonly IBuildingReadWriteRespository _buildingReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public BuildingDeleteViewModel(IBuildingReadWriteRespository buildingReadWriteRespository, IMapper mapper, ILocalizationService localizationService)
        {
            _buildingReadWriteRespository = buildingReadWriteRespository;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(BuildingDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _buildingReadWriteRespository.DeleteBuildingAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the Building"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "Building")
                    }
                };
            }
        }
    }
}
