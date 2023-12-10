using AutoMapper;
using BaseSolution.Application.DataTransferObjects.ServiceOrder;
using BaseSolution.Application.DataTransferObjects.ServiceOrder.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Infrastructure.ViewModels.ServiceOrder;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceOrdersController : ControllerBase
    {
        private readonly IServiceOrderReadOnlyRespository _serviceOrderReadOnly;
        private readonly ILocalizationService _localizationService;
        private readonly IValidator<ServiceOrderCreateRequest> _validator;
        private readonly IValidator<ServiceOrderUpdateRequest> _validatorUpdate;
        private readonly IValidator<ServiceOrderDeleteRequest> _validatorDelete;
        private readonly IMapper _mapper;
        private readonly IServiceOrderReadWriteRespository _serviceOrderReadWrite;
        public ServiceOrdersController(IServiceOrderReadOnlyRespository serviceOrderReadOnly, IServiceOrderReadWriteRespository serviceOrderReadWrite, IMapper mapper, ILocalizationService localizationService,
           IValidator<ServiceOrderCreateRequest> validator, IValidator<ServiceOrderUpdateRequest> validatorUpdate, IValidator<ServiceOrderDeleteRequest> validatorDelete)
        {
            _serviceOrderReadOnly = serviceOrderReadOnly;
            _serviceOrderReadWrite = serviceOrderReadWrite;
            _mapper = mapper;
            _localizationService = localizationService;
            _validator = validator;
            _validatorUpdate = validatorUpdate;
            _validatorDelete = validatorDelete;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceOrderById(Guid id, CancellationToken cancellationToken)
        {
            ServiceOrderViewModel vm = new(_serviceOrderReadOnly, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            if(vm.Success)
            {
                ServiceOrderDTO result = (ServiceOrderDTO)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpGet("serviceOrdersByAdmin")]
        public async Task<IActionResult> GetServiceOrdersByAdmin([FromQuery]ViewServiceOrderWithPaginationRequest request, CancellationToken cancellationToken)
        {
            ServiceOrderListWithPaginationViewModelByAdmin vm = new(_serviceOrderReadOnly, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("serviceOrdersByOther")]
        public async Task<IActionResult> GetServiceOrdersByOther([FromQuery]ViewServiceOrderWithPaginationRequest request, CancellationToken cancellationToken)
        {
            ServiceOrderListWithPaginationViewModelByOther vm = new(_serviceOrderReadOnly, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if(vm.Success)
            {
                PaginationResponse<ServiceOrderDTO> result = (PaginationResponse<ServiceOrderDTO>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpGet("ServiceOrdersByIdRoomBooking")]
        public async Task<IActionResult> GetServiceOrdersByIdRoomBooking(Guid idRoombooking, CancellationToken cancellationToken)
        {
            ServiceOrderListWithPaginationByIdRoomBookingViewModel vm = new(_serviceOrderReadOnly, _localizationService);
            await vm.HandleAsync(idRoombooking, cancellationToken);
            if(vm.Success)
            {
                List<ServiceOrderForRoomBookingDTO> result = (List<ServiceOrderForRoomBookingDTO>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewServiceOrder(ServiceOrderCreateRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validator.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }

            ServiceOrderCreateViewModel vm = new(_serviceOrderReadOnly, _serviceOrderReadWrite, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }

        [HttpPost("CreateNewServiceOrderForRoomBooking")]
        public async Task<IActionResult> CreateNewServiceOrderForRoomBooking(ServiceOrderCreateForRoomBookingRequest request, CancellationToken cancellationToken)
        {

            CreateNewServiceOrderForRoomBookingVM vm = new(_serviceOrderReadOnly, _serviceOrderReadWrite, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateServiceOrder (ServiceOrderUpdateRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validatorUpdate.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }

            ServiceOrderUpdateViewModel vm = new(_serviceOrderReadWrite, _mapper, _localizationService);
            await vm.HandleAsync (request, cancellationToken);
            return Ok(vm);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteServiceOrder(ServiceOrderDeleteRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validatorDelete.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }

            ServiceOrderDeleteViewModel vm = new(_serviceOrderReadWrite, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }

    }
}
