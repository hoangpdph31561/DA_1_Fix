using AutoMapper;
using Azure.Core;
using BaseSolution.Application.DataTransferObjects.Building.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;
using BaseSolution.Infrastructure.Implements.Repositories.ReadWrite;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.Building
{
    public class BuildingUpdateViewModel : ViewModelBase<BuildingUpdateRequest>
    {
        private readonly IBuildingReadWriteRespository _buildingReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public BuildingUpdateViewModel(IBuildingReadWriteRespository buildingReadWriteRespository, IMapper mapper, ILocalizationService localizationService)
        {
            _buildingReadWriteRespository = buildingReadWriteRespository;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(BuildingUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _buildingReadWriteRespository.UpdateBuildingAsync(_mapper.Map<BuildingEntity>(request), cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the building"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "Building")
                    }
                };
            }
        }
    }
}
