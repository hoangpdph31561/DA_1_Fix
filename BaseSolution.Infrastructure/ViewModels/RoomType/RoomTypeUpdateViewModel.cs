using AutoMapper;
using BaseSolution.Application.DataTransferObjects.RoomType.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.ViewModels.RoomType
{
    public class RoomTypeUpdateViewModel : ViewModelBase<RoomTypeUpdateRequest>
    {
        private readonly IRoomTypeReadWriteRepository _RoomTypeReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public RoomTypeUpdateViewModel(IRoomTypeReadWriteRepository RoomTypeReadWriteRepository, IMapper mapper, ILocalizationService localizationService)
        {
            _RoomTypeReadWriteRespository = RoomTypeReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async override Task HandleAsync(RoomTypeUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _RoomTypeReadWriteRespository.UpdateRoomTypeAsync(_mapper.Map<RoomTypeEntity>(request), cancellationToken);

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
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "RoomType")
                    }
                };
            }
        }
    }
}
