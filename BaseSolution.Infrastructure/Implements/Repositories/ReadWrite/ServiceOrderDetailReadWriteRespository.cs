﻿using BaseSolution.Application.DataTransferObjects.ServiceOrder.Request;
using BaseSolution.Application.DataTransferObjects.ServiceOrderDetail.Request;
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
    public class ServiceOrderDetailReadWriteRespository : IServiceOrderDetailReadWriteRespository
    {
        private readonly AppReadWriteDbContext _appReadWriteDbContext;
        private readonly ILocalizationService _localizationService;
        public ServiceOrderDetailReadWriteRespository(AppReadWriteDbContext appReadWriteDbContext, ILocalizationService localizationService)
        {
            _appReadWriteDbContext = appReadWriteDbContext;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<Guid>> CreateNewServiceOrderDetail(ServiceOrderDetailEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.CreatedTime = DateTimeOffset.UtcNow;

                await _appReadWriteDbContext.ServiceOrderDetails.AddAsync(entity);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create service order detail"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "service order detail"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> DeleteServiceOrderDetail(ServiceOrderDetailDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed serviceOrderDetail
                var serviceOrderDetail = await GetServiceOrderDetailByIdAsync(request.Id, cancellationToken);

                // Update value to existed serviceOrderDetail
                serviceOrderDetail!.Deleted = true;
                serviceOrderDetail.DeletedBy = request.DeletedBy;
                serviceOrderDetail.DeletedTime = DateTimeOffset.UtcNow;
                serviceOrderDetail.Status = EntityStatus.Deleted;

                _appReadWriteDbContext.ServiceOrderDetails.Update(serviceOrderDetail);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete service order detail"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "service order detail"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateServiceOrderDetail(ServiceOrderDetailEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed serviceOrderDetail
                var serviceOrderDetail = await GetServiceOrderDetailByIdAsync(entity.Id, cancellationToken);

                // Update value to existed serviceOrderDetail
                serviceOrderDetail!.ServiceId = entity.ServiceId;
                serviceOrderDetail.ServiceOrderId = entity.ServiceOrderId;
                serviceOrderDetail.Price = entity.Price;
                serviceOrderDetail.Amount = entity.Amount;

                serviceOrderDetail.ModifiedBy = entity.ModifiedBy;
                serviceOrderDetail.ModifiedTime = DateTimeOffset.UtcNow;

                _appReadWriteDbContext.ServiceOrderDetails.Update(serviceOrderDetail);
                await _appReadWriteDbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update service order detail"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "service order detail"
                    }
                });
            }
        }
        private async Task<ServiceOrderDetailEntity?> GetServiceOrderDetailByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _appReadWriteDbContext.ServiceOrderDetails.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            return result;
        }
    }
}
