using AutoMapper;
using Azure.Core;
using BaseSolution.Application.DataTransferObjects.RoomDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.RoomDetail
{
    public class RoomDetailUpdateViewModel : ViewModelBase<RoomDetailUpdateRequest>
    {
        public readonly IRoomDetailReadWriteRepository _RoomDetailReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public RoomDetailUpdateViewModel(IRoomDetailReadWriteRepository RoomDetailReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _RoomDetailReadWriteRepository = RoomDetailReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(RoomDetailUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _RoomDetailReadWriteRepository.UpdateRoomDetailAsync(_mapper.Map<RoomDetailEntity>(request), cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the RoomDetail"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "RoomDetail")
                    }
                };
            }
        }
    }
}
