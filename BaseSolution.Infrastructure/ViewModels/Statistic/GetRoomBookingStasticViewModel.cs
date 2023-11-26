using BaseSolution.Application.DataTransferObjects.Statistic.RoomBooking.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.ViewModels;
using BaseSolution.Infrastructure.Implements.Repositories.ReadOnly;
using BaseSolution.Infrastructure.Implements.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.Statistic
{
    public class GetRoomBookingStasticViewModel : ViewModelBase<ViewRoomBookingWithPaginationRequest>
    {
        private readonly IRoombookingStatisticReadOnlyRepository _roombookingStatisticReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public GetRoomBookingStasticViewModel(IRoombookingStatisticReadOnlyRepository roombookingStatisticReadOnlyRepository , ILocalizationService localizationService)
        {
            _roombookingStatisticReadOnlyRepository = roombookingStatisticReadOnlyRepository;
            _localizationService = localizationService;
        }

        public override async Task HandleAsync(ViewRoomBookingWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roombookingStatisticReadOnlyRepository.GetRoomBookingStasticAsync(request, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the list of roomBooking"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of roomBooking")
                }
            };
            }
        }
    }
}
