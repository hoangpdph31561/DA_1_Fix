using BaseSolution.Application.DataTransferObjects.Bill.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using BaseSolution.Domain.Enums;
using BaseSolution.Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Implements.Repositories.ReadWrite
{
    public class BillReadWriteRespository : IBillReadWriteRespository
    {
        private readonly AppReadWriteDbContext _appReadWriteDbContext;
        private readonly ILocalizationService _localizationService;
        public BillReadWriteRespository(AppReadWriteDbContext appReadWriteDbContext, ILocalizationService localizationService)
        {
            _appReadWriteDbContext = appReadWriteDbContext;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<Guid>> CreateNewBill(BillEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.CreatedTime = DateTimeOffset.UtcNow;
                entity.CustomerId = entity.CustomerId;

                await _appReadWriteDbContext.Bills.AddAsync(entity);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create bill"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "bill"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> DeleteBill(BillDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed bill
                var bill = await GetBillByIdAsync(request.Id, cancellationToken);

                // Update value to existed bill
                bill!.Deleted = true;
                bill.DeletedBy = request.DeletedBy;
                bill.DeletedTime = DateTimeOffset.UtcNow;
                bill.Status = EntityStatus.Deleted;

                _appReadWriteDbContext.Bills.Update(bill);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete bill"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "bill"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateBill(BillEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed bill
                var bill = await GetBillByIdAsync(entity.Id, cancellationToken);

                // Update value to existed bill
                bill!.CustomerId = entity.CustomerId;
                bill.ServiceOrderId = entity.ServiceOrderId;
                bill.RoomBookingId = entity.RoomBookingId;

                bill.ModifiedBy = entity.ModifiedBy;
                bill.ModifiedTime = DateTimeOffset.UtcNow;

                _appReadWriteDbContext.Bills.Update(bill);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update bill"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "bill"
                    }
                });
            }
        }
        private async Task<BillEntity?> GetBillByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var reuslt = await _appReadWriteDbContext.Bills.Where(x => x.Id == id && !x.Deleted).FirstOrDefaultAsync(cancellationToken);
            return reuslt;
        }
    }
}
