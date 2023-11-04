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

namespace BaseSolution.Infrastructure.ViewModels.Floor
{
    public class FloorViewModel : ViewModelBase<Guid>
    {
        private readonly IFloorReadOnlyRespository _floorReadOnlyRespository;
        private readonly ILocalizationService _localizationService;
        public FloorViewModel(IFloorReadOnlyRespository floorReadOnlyRespository, ILocalizationService localizationService)
        {
            _floorReadOnlyRespository = floorReadOnlyRespository;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _floorReadOnlyRespository.GetFloorByIdAsync(id, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the Floor"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Floor")
                }
            };
            }
        }
    }
}
