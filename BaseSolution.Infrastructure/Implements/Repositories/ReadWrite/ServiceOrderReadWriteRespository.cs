using BaseSolution.Application.DataTransferObjects.ServiceOrder.Request;
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
    public class ServiceOrderReadWriteRespository : IServiceOrderReadWriteRespository
    {
        private readonly AppReadWriteDbContext _appReadWriteDbContext;
        private readonly ILocalizationService _localizationService;
        public ServiceOrderReadWriteRespository(AppReadWriteDbContext appReadWriteDbContext, ILocalizationService localizationService)
        {
            _appReadWriteDbContext = appReadWriteDbContext;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<Guid>> CreateNewServiceOrder(ServiceOrderEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.CreatedTime = entity.CreatedTime;
                entity.CustomerId = entity.CustomerId;
                await _appReadWriteDbContext.ServiceOrders.AddAsync(entity);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create service order"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "service order"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> DeleteServiceOrder(ServiceOrderDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed serviceOrder
                var serviceOrder = await GetServiceOrderByIdAsync(request.Id, cancellationToken);

                // Update value to existed serviceOrder
                serviceOrder!.Deleted = true;
                serviceOrder.DeletedBy = request.DeletedBy;
                serviceOrder.DeletedTime = DateTimeOffset.UtcNow;
                serviceOrder.Status = EntityStatus.Deleted;

                _appReadWriteDbContext.ServiceOrders.Update(serviceOrder);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete service order"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "service order"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateServiceOrder(ServiceOrderEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed serviceOrder
                var serviceOrder = await GetServiceOrderByIdAsync(entity.Id, cancellationToken);

                // Update value to existed serviceOrder
                serviceOrder!.CustomerId = entity.CustomerId;
                serviceOrder.RoomBookingDetailId = entity.RoomBookingDetailId;
                serviceOrder.ModifiedBy = entity.ModifiedBy;
                serviceOrder.ModifiedTime = DateTimeOffset.UtcNow;
                serviceOrder.Status = entity.Status;
                _appReadWriteDbContext.ServiceOrders.Update(serviceOrder);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update service order"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "service order"
                    }
                });
            }
        }
        private async Task<ServiceOrderEntity?> GetServiceOrderByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _appReadWriteDbContext.ServiceOrders.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            return result;
        }
    }
}
