using AutoMapper;
using Azure.Core;
using BaseSolution.Application.DataTransferObjects.RoomDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
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
    public class RoomDetailDeleteViewModel : ViewModelBase<RoomDetailDeleteRequest>
    {
        public readonly IRoomDetailReadWriteRepository _RoomDetailReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public RoomDetailDeleteViewModel(IRoomDetailReadWriteRepository RoomDetailReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _RoomDetailReadWriteRepository = RoomDetailReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(RoomDetailDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _RoomDetailReadWriteRepository.DeleteRoomDetailAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the RoomDetail"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "RoomDetail")
                    }
                };
            }
        }
    }
}
