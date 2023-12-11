using AutoMapper;
using BaseSolution.Application.DataTransferObjects.AmenityRoomDetail.Request;
using BaseSolution.Application.DataTransferObjects.Bill;
using BaseSolution.Application.DataTransferObjects.Bill.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Infrastructure.ViewModels.Bill;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IBillReadOnlyRespository _billReadOnlyRespository;
        private readonly IBillReadWriteRespository _billReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        private readonly IValidator<BillCreateRequest> _validator;
        private readonly IValidator<BillUpdateRequest> _validatorUpdate;
        private readonly IValidator<BillDeleteRequest> _validatorDetete;

        public BillsController(IBillReadOnlyRespository billReadOnlyRespository, IBillReadWriteRespository billReadWriteRespository, ILocalizationService localizationService, IMapper mapper,
            IValidator<BillCreateRequest> validator, IValidator<BillUpdateRequest> validatorUpdate, IValidator<BillDeleteRequest> validatorDetete)

        {
            _billReadOnlyRespository = billReadOnlyRespository;
            _billReadWriteRespository = billReadWriteRespository;
            _mapper = mapper;
            _localizationService = localizationService;
            _validator = validator;
            _validatorUpdate = validatorUpdate;
            _validatorDetete = validatorDetete;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBillById(Guid id, CancellationToken cancellationToken)
        {
            BillViewModel vm = new(_billReadOnlyRespository, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            if (vm.Success)
            {
                BillDTO result = (BillDTO)vm.Data;

                return Ok(result);
            }
            return Ok(vm);
        }

         [HttpGet("GetBillByIdForRoom{id}")]
        public async Task<IActionResult> GetBillByIdForRoom(Guid id, CancellationToken cancellationToken)
        {
            BillViewModelForRoom vm = new(_billReadOnlyRespository, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            if (vm.Success)
            {
                BillDtoForRoom result = (BillDtoForRoom)vm.Data;
               return Ok(result);
            }
             return Ok(vm);
         }
        [HttpGet("{idCustomer}/details")]
        public async Task<IActionResult> GetBillByIdCustomer(Guid idCustomer, CancellationToken cancellationToken)
        {
            BillByCustomerIdViewModel vm = new(_billReadOnlyRespository, _localizationService);
            await vm.HandleAsync(idCustomer, cancellationToken);
            if (vm.Success)
            {
                List<BillDTO> result = (List<BillDTO>)vm.Data;

                return Ok(result);
            }
            
             return Ok(vm);
          }
          [HttpGet("GetBillByIdForService{id}")]
        public async Task<IActionResult> GetBillByIdForService(Guid id, CancellationToken cancellationToken)
        {
            BillViewModelForService vm = new(_billReadOnlyRespository, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            if (vm.Success)
            {
                BillDtoForService result = (BillDtoForService)vm.Data;

                return Ok(result);
            }
            return Ok(vm);
        }
        [HttpGet("getBillsByAdmin")]
        public async Task<IActionResult> GetBillsByAdmin([FromQuery] ViewBillWithPaginationRequest request, CancellationToken cancellationToken)
        {
            BillListWithPaginationByAdminViewModel vm = new(_billReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("getBillByOther")]
        public async Task<IActionResult> GetBillsByOther([FromQuery] ViewBillWithPaginationRequest request, CancellationToken cancellationToken)
        {
            BillListWithPaginationByOtherViewModel vm = new(_billReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);

            if (vm.Success)
            {
                PaginationResponse<BillDTO> result = (PaginationResponse<BillDTO>)vm.Data;

                return Ok(result);
            }
            return BadRequest(vm);
        }

        [HttpGet("getBillsByOtherForRoom")]
        public async Task<IActionResult> GetBillsByOtherForRoom([FromQuery] ViewBillWithPaginationRequest request, CancellationToken cancellationToken)
        {
            BillsByOtherForRoomViewModel vm = new(_billReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                PaginationResponse<BillDtoForRoom> result = (PaginationResponse<BillDtoForRoom>)vm.Data;

                return Ok(result);
            }
            return BadRequest(vm);
        }

         [HttpGet("getBillsByOtherForService")]
        public async Task<IActionResult> GetBillsByOtherForService([FromQuery] ViewBillWithPaginationRequest request, CancellationToken cancellationToken)
        {
            BillsByOtherForServiceViewModel vm = new(_billReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);

            if (vm.Success)
            {
                PaginationResponse<BillDtoForService> result = (PaginationResponse<BillDtoForService>)vm.Data;

                return Ok(result);
            }
            return BadRequest(vm);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBill(BillCreateRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validator.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            BillCreateViewModel vm = new(_billReadOnlyRespository, _billReadWriteRespository, _localizationService, _mapper);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBill(BillUpdateRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validatorUpdate.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            BillUpdateViewModel vm = new(_billReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBill(BillDeleteRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validatorDetete.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            BillDeleteViewModel vm = new(_billReadWriteRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
    }
}
