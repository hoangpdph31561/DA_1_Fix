﻿using AutoMapper;
using Azure.Core;
using BaseSolution.Application.DataTransferObjects.RoomDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
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
    public class RoomDetailCreateViewModel : ViewModelBase<RoomDetailCreateRequest>
    {
        public readonly IRoomDetailReadOnlyRepository _RoomDetailReadOnlyRepository;
        public readonly IRoomDetailReadWriteRepository _RoomDetailReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public RoomDetailCreateViewModel(IRoomDetailReadOnlyRepository RoomDetailReadOnlyRepository, IRoomDetailReadWriteRepository RoomDetailReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _RoomDetailReadOnlyRepository = RoomDetailReadOnlyRepository;
            _RoomDetailReadWriteRepository = RoomDetailReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(RoomDetailCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _RoomDetailReadWriteRepository.AddRoomDetailAsync(_mapper.Map<RoomDetailEntity>(request), cancellationToken);

                if (createResult.Success)
                {
                    var result = await _RoomDetailReadOnlyRepository.GetRoomDetailByIdAsync(createResult.Data, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the RoomDetail"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "RoomDetail")
                    }
                };
            }
        }
    }
}
