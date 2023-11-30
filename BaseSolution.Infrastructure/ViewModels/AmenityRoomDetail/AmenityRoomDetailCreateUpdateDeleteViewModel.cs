using BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.AmenityRoomDetail
{
    public class AmenityRoomDetailCreateUpdateDeleteViewModel : ViewModelBase<List<AmenityCreateUpdateDeleteRequest>>
    {
        public readonly IAmenityRoomDetailReadWriteRepository _amenityRoomDetailReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        public AmenityRoomDetailCreateUpdateDeleteViewModel(IAmenityRoomDetailReadWriteRepository amenityRoomDetailReadWriteRepository, ILocalizationService localizationService)
        {
            _amenityRoomDetailReadWriteRepository = amenityRoomDetailReadWriteRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(List<AmenityCreateUpdateDeleteRequest> request, CancellationToken cancellationToken)
        {
			try
			{
                var result = await _amenityRoomDetailReadWriteRepository.CreateUpdateDeleteAmenityRoomDetailAsync(request, cancellationToken);
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
                        Error = _localizationService["Error occurred while updating the AmenityRoomDetail"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "AmenityRoomDetail")
                    }
                };
            }
        }
    }
}
