using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Infrastructure.Implements.Repositories.ReadOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.Bill
{
    public class BillViewModel : ViewModelBase<Guid>
    {
        private readonly IBillReadOnlyRespository _billReadOnlyRespository;
        private readonly ILocalizationService _localizationService;
        public BillViewModel(IBillReadOnlyRespository billReadOnlyRespository, ILocalizationService localizationService)
        {
            _billReadOnlyRespository = billReadOnlyRespository;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _billReadOnlyRespository.GetBillByIdAsync(id, cancellationToken);

                Data = result.Data!;
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
                    Error = _localizationService["Error occurred while getting the Bill"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Bill")
                }
            };
            }
        }
    }
}
