using BaseSolution.Application.DataTransferObjects.RoomBookingDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Infrastructure.Implements.Repositories.ReadOnly;
using BaseSolution.Infrastructure.Implements.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.RoomBookingDetail
{
    public class RoomBookingDetailByRoomBookingIdViewModel : ViewModelBase<Guid>
    {
        private readonly IRoomBookingDetailReadOnlyRepository _roomBookingDetailReadOnly;
        private readonly ILocalizationService _localizationService;

        public RoomBookingDetailByRoomBookingIdViewModel(IRoomBookingDetailReadOnlyRepository roomBookingDetailReadOnly , ILocalizationService localizationService)
        {
            _roomBookingDetailReadOnly = roomBookingDetailReadOnly;
            _localizationService = localizationService;
        }

        public async override Task HandleAsync(Guid idRoomBooking, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roomBookingDetailReadOnly.GetRoomBookingDetailWithPaginationByIdRoomBookingAsync(idRoomBooking, cancellationToken);

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
