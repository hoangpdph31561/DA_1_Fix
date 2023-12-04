﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseSolution.Application.DataTransferObjects.Roombooking;
using BaseSolution.Application.DataTransferObjects.Roombooking.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using BaseSolution.Domain.Enums;
using BaseSolution.Infrastructure.Database.AppDbContext;
using BaseSolution.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BaseSolution.Infrastructure.Implements.Repositories.ReadOnly
{
    public class RoombookingReadOnlyRepository : IRoombookingReadOnlyRepository
    {
        private readonly AppReadOnlyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public RoombookingReadOnlyRepository(AppReadOnlyDbContext appReadOnlyDbContext, IMapper mapper, ILocalizationService localizationService)
        {
            _dbContext = appReadOnlyDbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<RoombookingDTO?>> GetRoombookingByIdAsync(Guid idRoombooking, CancellationToken cancellationToken)
        {
            try
            {
                var roombooking = await _dbContext.RoomBookings.AsNoTracking().Where(x => x.Id == idRoombooking).ProjectTo<RoombookingDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
                return RequestResult<RoombookingDTO?>.Succeed(roombooking);

            }
            catch (Exception e)
            {

                return RequestResult<RoombookingDTO?>.Fail(_localizationService["Roombooking is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "roombooking"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<RoombookingDTO>>> GetRoombookingWithPaginationByAdminAsync(ViewRoombookingWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _dbContext.RoomBookings.AsNoTracking().ProjectTo<RoombookingDTO>(_mapper.ConfigurationProvider);
                
                if(!string.IsNullOrWhiteSpace(request.SearchString))
                {
                    query = query.Where(x => x.NameCustomer.Contains(request.SearchString!));
                }
                var result = await query.PaginateAsync(request, cancellationToken);
                foreach (var item in result.Data!)
                {
                    item.ServiceAmount = item.TotalService * item.ServicePrice;
                    item.RoomAmount = UtilityExtensions.TinhTien(item.CheckInReality, item.CheckOutReality, item.RoomPrice, item.PrePaid);
                    item.TotalAmount = item.ServiceAmount + item.RoomAmount;
                }
                return RequestResult<PaginationResponse<RoombookingDTO>>.Succeed(new PaginationResponse<RoombookingDTO>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {

                return RequestResult<PaginationResponse<RoombookingDTO>>.Fail(_localizationService["List of roombooking are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error= e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of roombooking"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<RoombookingDTO>>> GetRoombookingWithPaginationByOtherAsync(ViewRoombookingWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _dbContext.RoomBookings.AsNoTracking().Where(x => x.Deleted == false && x.Status != EntityStatus.Deleted).ProjectTo<RoombookingDTO>(_mapper.ConfigurationProvider);

                if (!string.IsNullOrWhiteSpace(request.SearchString))
                {
                    query = query.Where(x => x.NameCustomer.Contains(request.SearchString!));
                }
                var result = await query.PaginateAsync(request, cancellationToken);

                foreach (var item in result.Data!)
                {
                    item.ServiceAmount = item.TotalService * item.ServicePrice;
                    item.RoomAmount = UtilityExtensions.TinhTien(item.CheckInReality, item.CheckOutReality, item.RoomPrice, item.PrePaid);
                    item.TotalAmount = item.ServiceAmount + item.RoomAmount;
                }
                return RequestResult<PaginationResponse<RoombookingDTO>>.Succeed(new PaginationResponse<RoombookingDTO>
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {

                return RequestResult<PaginationResponse<RoombookingDTO>>.Fail(_localizationService["List of roombooking are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error= e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of roombooking"
                    }
                });
            }
        }
    }
}
