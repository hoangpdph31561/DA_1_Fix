using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.RoomDetail
{
    public class RoomDetailViewModelByRoomTypeId : ViewModelBase<Guid>
    {
        public readonly IRoomDetailReadOnlyRepository _RoomDetailReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public RoomDetailViewModelByRoomTypeId(IRoomDetailReadOnlyRepository RoomDetailReadOnlyRepository, ILocalizationService localizationService)
        {
            _RoomDetailReadOnlyRepository = RoomDetailReadOnlyRepository;
            _localizationService = localizationService;
        }

        public override async Task HandleAsync(Guid idRoomType, CancellationToken cancellationToken)
        {


            try
            {
                var result = await _RoomDetailReadOnlyRepository.GetRoomDetailByIdRoomTypeAsync(idRoomType, cancellationToken);

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
