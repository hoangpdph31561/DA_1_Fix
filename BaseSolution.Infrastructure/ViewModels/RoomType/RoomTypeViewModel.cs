using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.RoomType
{
    public class RoomTypeViewModel : ViewModelBase<Guid>
    {
        private readonly IRoomTypeReadOnlyRepository _RoomTypeReadOnlyRespository;
        private readonly ILocalizationService _localizationService;
        public RoomTypeViewModel(IRoomTypeReadOnlyRepository RoomTypeReadOnlyRepository, ILocalizationService localizationService)
        {
            _RoomTypeReadOnlyRespository = RoomTypeReadOnlyRepository;
            _localizationService = localizationService;
        }

        public async override Task HandleAsync(Guid idRoomType, CancellationToken cancellationToken)
        {

            try
            {
                var result = await _RoomTypeReadOnlyRespository.GetRoomTypeByIdAsync(idRoomType, cancellationToken);
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
                        Error = _localizationService["Error occurred while getting the RoomType"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "RoomType")
                    }
                };
            }
        }
    }
}
