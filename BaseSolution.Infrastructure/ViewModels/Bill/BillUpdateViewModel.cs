using AutoMapper;
using Azure.Core;
using BaseSolution.Application.DataTransferObjects.Bill.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;
using BaseSolution.Infrastructure.Implements.Repositories.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.Bill
{
    public class BillUpdateViewModel : ViewModelBase<BillUpdateRequest>
    {
        private readonly IBillReadWriteRespository _billReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public BillUpdateViewModel(IBillReadWriteRespository billReadWriteRespository, IMapper mapper, ILocalizationService localizationService)
        {
            _billReadWriteRespository = billReadWriteRespository;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(BillUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _billReadWriteRespository.UpdateBill(_mapper.Map<BillEntity>(request), cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the Bill"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "Bill")
                    }
                };
            }
        }
    }
}
