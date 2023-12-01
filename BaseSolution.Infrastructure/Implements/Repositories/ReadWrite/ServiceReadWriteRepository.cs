using BaseSolution.Application.DataTransferObjects.Services.Request;
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
    public class ServiceReadWriteRepository : IServicesReadWriteRepository
    {
        private readonly AppReadWriteDbContext _dbContext;
        private readonly ILocalizationService _localizationService;

        public ServiceReadWriteRepository(ILocalizationService localizationService, AppReadWriteDbContext dbContext)
        {
            _localizationService = localizationService;
            _dbContext = dbContext;
        }
        public async Task<RequestResult<Guid>> AddServiceAsync(ServiceEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.CreatedTime = DateTimeOffset.UtcNow;

                await _dbContext.Services.AddAsync(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create Service"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "Service"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> DeleteServiceAsync(ServiceDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed Service
                var Service = await GetServiceByIdAsync(request.Id, cancellationToken);

                // Update value to existed Service
                Service!.Deleted = true;
                Service.DeletedBy = request.DeletedBy;
                Service.DeletedTime = DateTimeOffset.UtcNow;
                Service.Status = EntityStatus.Deleted;

                _dbContext.Services.Update(Service);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete Service"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "Service"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateServiceAsync(ServiceEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed Service
                var Service = await GetServiceByIdAsync(entity.Id, cancellationToken);

                // Update value to existed Service
                Service!.Name = string.IsNullOrWhiteSpace(entity.Name) ? Service.Name : entity.Name;
                Service.Status = entity.Status;
                Service.Description = entity.Description;
                Service.Unit = entity.Unit;
                Service.ModifiedBy = entity.ModifiedBy;
                Service.ModifiedTime = DateTimeOffset.UtcNow;
                Service.ServiceTypeId = entity.ServiceTypeId;
                Service.IsRoomBookingNeed = entity.IsRoomBookingNeed;
                _dbContext.Services.Update(Service);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update Service"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "Service"
                    }
                });
            }
        }
        private async Task<ServiceEntity?> GetServiceByIdAsync(Guid idService, CancellationToken cancellationToken)
        {
            var Service = await _dbContext.Services.FirstOrDefaultAsync(c => c.Id == idService && !c.Deleted, cancellationToken);

            return Service;
        }
    }
}
