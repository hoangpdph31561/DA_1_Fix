using AutoMapper;
using BaseSolution.Application.DataTransferObjects.RoomType.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;

namespace BaseSolution.Infrastructure.ViewModels.RoomType
{
    public class RoomTypeCreateViewModel : ViewModelBase<RoomTypeCreateRequest>
    {
        public readonly IRoomTypeReadOnlyRepository _RoomTypeReadOnlyRepository;
        public readonly IRoomTypeReadWriteRepository _RoomTypeReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public RoomTypeCreateViewModel(IRoomTypeReadOnlyRepository RoomTypeReadOnlyRepository, IRoomTypeReadWriteRepository RoomTypeReadWriteRepository, IMapper mapper, ILocalizationService localizationService)
        {
            _RoomTypeReadOnlyRepository = RoomTypeReadOnlyRepository;
            _RoomTypeReadWriteRespository = RoomTypeReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async override Task HandleAsync(RoomTypeCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _RoomTypeReadWriteRespository.AddRoomTypeAsync(_mapper.Map<RoomTypeEntity>(request), cancellationToken);
                if (createResult.Success)
                {
                    var result = await _RoomTypeReadOnlyRepository.GetRoomTypeByIdAsync(createResult.Data, cancellationToken);

                    Data = result.Data!;
                    Success = result.Success;
                    ErrorItems = result.Errors;
                    Message = result.Message;
                    return;
                }

                Success = createResult.Success;
                ErrorItems = createResult.Errors;
                Message = createResult.Message;
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
