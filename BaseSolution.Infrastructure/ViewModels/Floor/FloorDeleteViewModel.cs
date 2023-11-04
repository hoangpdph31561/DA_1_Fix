using AutoMapper;
using Azure.Core;
using BaseSolution.Application.DataTransferObjects.Floor.Request;
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

namespace BaseSolution.Infrastructure.ViewModels.Floor
{
    public class FloorDeleteViewModel : ViewModelBase<FloorDeleteRequest>
    {
        private readonly IFloorReadWriteRespository _floorReadWriteRespository;
        private readonly ILocalizationService _localizationService;
        public FloorDeleteViewModel(IFloorReadWriteRespository floorReadWriteRespository, ILocalizationService localizationService)
        {
            _floorReadWriteRespository = floorReadWriteRespository;
            _localizationService = localizationService;
        }

        public async override Task HandleAsync(FloorDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _floorReadWriteRespository.DeleteFloorAsync(request, cancellationToken);

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
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "Floor")
                    }
                };
            }
        }
    }
}
