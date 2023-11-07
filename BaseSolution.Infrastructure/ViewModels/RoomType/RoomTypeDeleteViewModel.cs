using AutoMapper;
using BaseSolution.Application.DataTransferObjects.RoomType.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.RoomType
{
    public class RoomTypeDeleteViewModel : ViewModelBase<RoomTypeDeleteRequest>
    {
        public readonly IRoomTypeReadWriteRepository _RoomTypeReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        public RoomTypeDeleteViewModel(IRoomTypeReadWriteRepository RoomTypeReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _RoomTypeReadWriteRepository = RoomTypeReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public async override Task HandleAsync(RoomTypeDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _RoomTypeReadWriteRepository.DeleteRoomTypeAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the RoomType"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "RoomType")
                    }
                };
            }
        }
    }
}
