using BaseSolution.Application.DataTransferObjects.Example.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using BaseSolution.Domain.Enums;
using BaseSolution.Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace BaseSolution.Infrastructure.Implements.Repositories.ReadWrite
{
    public class ExampleReadWriteRepository : IExampleReadWriteRepository
    {
        private readonly ExampleReadWriteDbContext _dbContext;
        private readonly ILocalizationService _localizationService;

        public ExampleReadWriteRepository(ILocalizationService localizationService, ExampleReadWriteDbContext dbContext)
        {
            _localizationService = localizationService;
            _dbContext = dbContext;
        }
        public async Task<RequestResult<Guid>> AddExampleAsync(ExampleEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.CreatedTime = DateTimeOffset.UtcNow;

                await _dbContext.Examples.AddAsync(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create example"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "example"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> DeleteExampleAsync(ExampleDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed example
                var example = await GetExampleByIdAsync(request.Id, cancellationToken);

                // Update value to existed example
                example!.Deleted = true;
                example.DeletedBy = request.DeletedBy;
                example.DeletedTime = DateTimeOffset.UtcNow;
                example.Status = EntityStatus.Deleted;

                _dbContext.Examples.Update(example);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete example"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "example"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateExampleAsync(ExampleEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed example
                var example = await GetExampleByIdAsync(entity.Id, cancellationToken);

                // Update value to existed example
                example!.Name = string.IsNullOrWhiteSpace(entity.Name) ? example.Name : entity.Name;

                example.ModifiedBy = entity.ModifiedBy;
                example.ModifiedTime = DateTimeOffset.UtcNow;

                _dbContext.Examples.Update(example);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update example"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "example"
                    }
                });
            }
        }
        private async Task<ExampleEntity?> GetExampleByIdAsync(Guid idExample, CancellationToken cancellationToken)
        {
            var example = await _dbContext.Examples.FirstOrDefaultAsync(c => c.Id == idExample && !c.Deleted, cancellationToken);

            return example;
        }
    }
}