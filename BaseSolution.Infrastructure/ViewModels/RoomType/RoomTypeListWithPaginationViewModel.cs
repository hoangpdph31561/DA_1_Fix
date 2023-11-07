using BaseSolution.Application.DataTransferObjects.RoomType.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.RoomType
{
    public class RoomTypeListWithPaginationViewModel : ViewModelBase<ViewRoomTypeWithPaginationRequest>
    {
        private readonly IRoomTypeReadOnlyRepository _RoomTypeReadOnlyRepository;
        private readonly ILocalizationService _localizationService;
        public RoomTypeListWithPaginationViewModel(IRoomTypeReadOnlyRepository RoomTypeReadOnlyRepository, ILocalizationService localizationService)
        {
            _RoomTypeReadOnlyRepository = RoomTypeReadOnlyRepository;
            _localizationService = localizationService;
        }

        public async override Task HandleAsync(ViewRoomTypeWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _RoomTypeReadOnlyRepository.GetRoomTypeWithPaginationByAdminAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the list of RoomType"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of RoomType")
                    }
                };
            }
        }
    }
}
