using Azure.Core;
using BaseSolution.Application.DataTransferObjects.Bill.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Infrastructure.Implements.Repositories.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.Bill
{
    public class BillDeleteViewModel : ViewModelBase<BillDeleteRequest>
    {
        private readonly IBillReadWriteRespository _billReadWriteRespository;
        private readonly ILocalizationService _localizationService;
        public BillDeleteViewModel(IBillReadWriteRespository billReadWriteRespository, ILocalizationService localizationService)
        {
            _billReadWriteRespository = billReadWriteRespository;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(BillDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _billReadWriteRespository.DeleteBill(request, cancellationToken);

                Success = result.Success;
                ErrorItems = result.Errors;
                Message = result.Message;
                return;
            }
            catch (Exception)
            {
                Success = false;
                ErrorItems = new[]
                    {
                    new ErrorItem
                    {
                        Error = _localizationService["Error occurred while updating the Bill"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "bill")
                    }
                };
            }
        }
    }
}
