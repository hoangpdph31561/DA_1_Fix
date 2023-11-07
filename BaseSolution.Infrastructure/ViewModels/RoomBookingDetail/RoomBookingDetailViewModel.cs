using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.RoomBookingDetail
{
    public class RoomBookingDetailViewModel : ViewModelBase<Guid>
    {
        private readonly IRoomBookingDetailReadOnlyRepository _roombookingdetailReadOnlyRespository;
        private readonly ILocalizationService _localizationService;
        public RoomBookingDetailViewModel(IRoomBookingDetailReadOnlyRepository roomBookingDetailReadOnlyRepository, ILocalizationService localizationService)
        {
            _roombookingdetailReadOnlyRespository = roomBookingDetailReadOnlyRepository;
            _localizationService = localizationService;
        }

        public async override Task HandleAsync(Guid idRoomBookingDetail, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roombookingdetailReadOnlyRespository.GetRoomBookingDetailByIdAsync(idRoomBookingDetail, cancellationToken);
                Data = result.Data!;
                Success = result.Success;
                ErrorItems = result.Errors;
                Message = result.Message;
                return;
            }
            catch(Exception)
            {
                Success = false;
                ErrorItems = new[]
                {
                    new ErrorItem
                    {
                        Error = _localizationService["Error occurred while getting the RoomBookingDetail"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "RoomBookingDetail")
                    }
                };
            }
        }
    }
}
