using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseSolution.Application.DataTransferObjects.Bill;
using BaseSolution.Application.DataTransferObjects.Bill.Request;
using BaseSolution.Application.DataTransferObjects.Example;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using BaseSolution.Infrastructure.Database.AppDbContext;
using BaseSolution.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Implements.Repositories.ReadOnly
{
    public class BillReadOnlyRespository : IBillReadOnlyRespository
    {
        private readonly AppReadOnlyDbContext _appReadOnlyDbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public BillReadOnlyRespository(AppReadOnlyDbContext appReadOnlyDbContext, IMapper mapper, ILocalizationService localizationService)
        {
            _appReadOnlyDbContext = appReadOnlyDbContext;   
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<BillDTO?>> GetBillByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var bill = await _appReadOnlyDbContext.Bills.AsNoTracking().Where(c => c.Id == id && !c.Deleted).ProjectTo<BillDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
                var userCreated = await _appReadOnlyDbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == bill!.CreatedBy, cancellationToken) == null ? "N/A" : _appReadOnlyDbContext.Users.AsNoTracking().First(x => x.Id == bill!.CreatedBy)!.Name;
                bill!.CreatedUserName = userCreated;
                return RequestResult<BillDTO?>.Succeed(bill);
            }
            catch (Exception e)
            {
                return RequestResult<BillDTO?>.Fail(_localizationService["Bill is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "Bill"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<BillDTO>>> GetBillsByAdminAsync(ViewBillWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<BillEntity> queryable = _appReadOnlyDbContext.Bills.AsNoTracking().AsQueryable();
                var result = await _appReadOnlyDbContext.Bills.AsNoTracking()
                    .PaginateAsync<BillEntity, BillDTO>(request, _mapper, cancellationToken);
                foreach (var item in result.Data!)
                {
                    var userCreated = await _appReadOnlyDbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.CreatedBy, cancellationToken) == null ? "N/A" : _appReadOnlyDbContext.Users.AsNoTracking().First(x => x.Id == item.CreatedBy)!.Name;
                    item.CreatedUserName = userCreated;
                }
                return RequestResult<PaginationResponse<BillDTO>>.Succeed(new PaginationResponse<BillDTO>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<BillDTO>>.Fail(_localizationService["List of Bill are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of bill"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<BillDTO>>> GetBillsByOtherAsync(ViewBillWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<BillEntity> queryable = _appReadOnlyDbContext.Bills.AsNoTracking().AsQueryable();
                var result = await _appReadOnlyDbContext.Bills.AsNoTracking().Where(x => !x.Deleted)
                    .PaginateAsync<BillEntity, BillDTO>(request, _mapper, cancellationToken);
                foreach (var item in result.Data!)
                {
                    var userCreated = await _appReadOnlyDbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.CreatedBy, cancellationToken) == null ? "N/A" : _appReadOnlyDbContext.Users.AsNoTracking().First(x => x.Id == item.CreatedBy)!.Name;
                    item.CreatedUserName = userCreated;
                }
                return RequestResult<PaginationResponse<BillDTO>>.Succeed(new PaginationResponse<BillDTO>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<BillDTO>>.Fail(_localizationService["List of Bill are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of bill"
                    }
                });
            }
        }
    }
}
