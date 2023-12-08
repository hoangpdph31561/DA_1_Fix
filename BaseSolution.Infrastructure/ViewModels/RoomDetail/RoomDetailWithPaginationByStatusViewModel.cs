using BaseSolution.Application.DataTransferObjects.RoomDetail.Request;
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

namespace BaseSolution.Infrastructure.ViewModels.RoomDetail
{
    public class RoomDetailWithPaginationByStatusViewModel : ViewModelBase<ViewRoomDetailByCheckInCheckOutRequest>
    {
        private readonly IRoomDetailReadOnlyRepository _roomDetailReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public RoomDetailWithPaginationByStatusViewModel(IRoomDetailReadOnlyRepository roomDetailReadOnlyRepository, ILocalizationService localizationService)
        {
            _roomDetailReadOnlyRepository = roomDetailReadOnlyRepository;
            _localizationService = localizationService;
         }
        public override async Task HandleAsync(ViewRoomDetailByCheckInCheckOutRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roomDetailReadOnlyRepository.GetRoomDetailWithPaginationByStatusAsync(data, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the RoomDetail"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "RoomDetail")
                }
            };
            }
        }
    }
}
