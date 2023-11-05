using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Bill.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;
using BaseSolution.Domain.Entities;
using BaseSolution.Infrastructure.Implements.Repositories.ReadOnly;
using BaseSolution.Infrastructure.Implements.Repositories.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.ViewModels.Bill
{
    public class BillCreateViewModel : ViewModelBase<BillCreateRequest>
    {
        private readonly IBillReadOnlyRespository _billReadOnlyRespository;
        private readonly IBillReadWriteRespository _billReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public BillCreateViewModel(IBillReadOnlyRespository billReadOnlyRespository, IBillReadWriteRespository billReadWriteRespository, ILocalizationService localizationService, IMapper mapper)
        {
            _billReadOnlyRespository = billReadOnlyRespository;
            _billReadWriteRespository = billReadWriteRespository;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(BillCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _billReadWriteRespository.CreateNewBill(_mapper.Map<BillEntity>(request), cancellationToken);

                if (createResult.Success)
                {
                    var result = await _billReadOnlyRespository.GetBillByIdAsync(createResult.Data, cancellationToken);

                    Data = result.Data!;
                    Success = result.Success;
                    ErrorItems = result.Errors;
                    Message = result.Message;
                    return;
                }

                Success = createResult.Success;
                ErrorItems = createResult.Errors;
                Message = createResult.Message;
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
