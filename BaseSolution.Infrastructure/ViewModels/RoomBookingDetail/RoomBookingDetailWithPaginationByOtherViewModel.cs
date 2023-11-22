using Azure.Core;
using BaseSolution.Application.DataTransferObjects.RoomBookingDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.RoomBookingDetail
{
    public class RoomBookingDetailWithPaginationByOtherViewModel : ViewModelBase<ViewRoomBookingDetailWithPaginationRequest>
    {
        private readonly IRoomBookingDetailReadOnlyRepository _roombookingdetailReadOnlyRepository;
        private readonly ILocalizationService _localizationService;
        public RoomBookingDetailWithPaginationByOtherViewModel(IRoomBookingDetailReadOnlyRepository roomBookingDetailReadOnlyRepository, ILocalizationService localizationService)
        {
            _roombookingdetailReadOnlyRepository = roomBookingDetailReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(ViewRoomBookingDetailWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roombookingdetailReadOnlyRepository.GetRoomBookingDetailWithPaginationByOtherAsync(request, cancellationToken);

                Data = result.Data!;
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
                        Error = _localizationService["Error occurred while getting the list of RoomBookingDetail"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of RoomBookingDetail")
                    }
                };
            }
        }
    }
}
