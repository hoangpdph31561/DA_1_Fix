using BaseSolution.Application.DataTransferObjects.ServiceType.Request;
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
    public class ServiceTypeReadWriteRepository : IServiceTypeReadWriteRepository
    {
        private readonly AppReadWriteDbContext _dbContext;
        private readonly ILocalizationService _localizationService;

        public ServiceTypeReadWriteRepository(ILocalizationService localizationService, AppReadWriteDbContext dbContext)
        {
            _localizationService = localizationService;
            _dbContext = dbContext;
        }
        public async Task<RequestResult<Guid>> AddServiceTypeAsync(ServiceTypeEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.CreatedTime = DateTimeOffset.UtcNow;

                await _dbContext.ServiceTypes.AddAsync(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create ServiceType"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "ServiceType"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> DeleteServiceTypeAsync(ServiceTypeDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed ServiceType
                var ServiceType = await GetServiceTypeByIdAsync(request.Id, cancellationToken);

                // Update value to existed ServiceType
                ServiceType!.Deleted = true;
                ServiceType.DeletedBy = request.DeletedBy;
                ServiceType.DeletedTime = DateTimeOffset.UtcNow;
                ServiceType.Status = EntityStatus.Deleted;

                _dbContext.ServiceTypes.Update(ServiceType);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete ServiceType"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "ServiceType"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateServiceTypeAsync(ServiceTypeEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed ServiceType
                var ServiceType = await GetServiceTypeByIdAsync(entity.Id, cancellationToken);

                // Update value to existed ServiceType
                ServiceType!.Name = string.IsNullOrWhiteSpace(entity.Name) ? ServiceType.Name : entity.Name;
                ServiceType.Status = entity.Status;
                ServiceType.ModifiedBy = entity.ModifiedBy;
                ServiceType.ModifiedTime = DateTimeOffset.UtcNow;

                _dbContext.ServiceTypes.Update(ServiceType);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update ServiceType"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "ServiceType"
                    }
                });
            }
        }
        private async Task<ServiceTypeEntity?> GetServiceTypeByIdAsync(Guid idServiceType, CancellationToken cancellationToken)
        {
            var ServiceType = await _dbContext.ServiceTypes.FirstOrDefaultAsync(c => c.Id == idServiceType && !c.Deleted, cancellationToken);

            return ServiceType;
        }
    }
}
