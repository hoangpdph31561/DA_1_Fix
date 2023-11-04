using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Floor.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;
using BaseSolution.Infrastructure.Implements.Repositories.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.Floor
{
    public class FloorUpdateViewModel : ViewModelBase<FloorUpdateRequest>
    {
        private readonly IFloorReadWriteRespository _floorReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public FloorUpdateViewModel(IFloorReadWriteRespository floorReadWriteRespository, IMapper mapper, ILocalizationService localizationService)
        {
            _floorReadWriteRespository = floorReadWriteRespository;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(FloorUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _floorReadWriteRespository.UpdateFloorAsync(_mapper.Map<FloorEntity>(request), cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the Floor"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "Floor")
                    }
                };
            }
        }
    }
}
