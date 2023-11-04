using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Floor.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;
using BaseSolution.Infrastructure.Implements.Repositories.ReadOnly;
using BaseSolution.Infrastructure.Implements.Repositories.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.Floor
{
    public class FloorCreateViewModel : ViewModelBase<FloorCreateRequest>
    {
        private readonly IFloorReadOnlyRespository _floorReadOnlyRespository;
        private readonly IFloorReadWriteRespository _floorReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public FloorCreateViewModel(IFloorReadOnlyRespository floorReadOnlyRespository, IFloorReadWriteRespository floorReadWriteRespository, IMapper mapper, ILocalizationService localizationService)
        {
            _floorReadOnlyRespository = floorReadOnlyRespository;
            _floorReadWriteRespository = floorReadWriteRespository;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(FloorCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _floorReadWriteRespository.AddFloorAsync(_mapper.Map<FloorEntity>(request), cancellationToken);

                if (createResult.Success)
                {
                    var result = await _floorReadOnlyRespository.GetFloorByIdAsync(createResult.Data, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the Floor"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Floor")
                    }
                };
            }
        }
    }
}
