using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Role.Request;
using BaseSolution.Application.DataTransferObjects.ServiceType;
using BaseSolution.Application.DataTransferObjects.ServiceType.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Infrastructure.ViewModels.ServiceType;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypesController : ControllerBase
    {
        public readonly IServiceTypeReadOnlyRepository _ServiceTypeReadOnlyRepository;
        public readonly IServiceTypeReadWriteRepository _ServiceTypeReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        private readonly IValidator<ServiceTypeCreateRequest> _validator;
        private readonly IValidator<ServiceTypeUpDateRequest> _validatorUpdate;
        private readonly IValidator<ServiceTypeDeleteRequest> _validatorDelete;

        public ServiceTypesController(IServiceTypeReadOnlyRepository ServiceTypeReadOnlyRepository, IServiceTypeReadWriteRepository ServiceTypeReadWriteRepository, ILocalizationService localizationService, IMapper mapper,
              IValidator<ServiceTypeCreateRequest> validator, IValidator<ServiceTypeUpDateRequest> validatorUpdate, IValidator<ServiceTypeDeleteRequest> validatorDelete

            )
        {
            _ServiceTypeReadOnlyRepository = ServiceTypeReadOnlyRepository;
            _ServiceTypeReadWriteRepository = ServiceTypeReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
            _validator = validator;
            _validatorUpdate = validatorUpdate;
            _validatorDelete = validatorDelete;
        }

        // GET api/<ServiceTypeController>/5
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ViewServiceTypeWithPaginationRequest request, CancellationToken cancellationToken)
        {
            ServiceTypeListWithPaginationViewModel vm = new(_ServiceTypeReadOnlyRepository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                PaginationResponse<ServiceTypeDto> result = (PaginationResponse<ServiceTypeDto>)vm.Data;
                return Ok(result);
            }

            return BadRequest(vm);
        }

        // GET api/<ServiceTypeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            ServiceTypeViewModel vm = new(_ServiceTypeReadOnlyRepository, _localizationService);

            await vm.HandleAsync(id, cancellationToken);
            if (vm.Success) 
            {
                ServiceTypeDto serviceTypeDto = (ServiceTypeDto)vm.Data;
                return Ok(serviceTypeDto);
            }

            return BadRequest(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ServiceTypeCreateRequest request, CancellationToken cancellationToken)
        {
            ServiceTypeCreateViewModel vm = new(_ServiceTypeReadOnlyRepository, _ServiceTypeReadWriteRepository, _localizationService, _mapper);
            ValidationResult validate = await _validator.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }

            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                return Ok(vm);
            }

            return BadRequest(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ServiceTypeUpDateRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validatorUpdate.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            ServiceTypeUpdateViewModel vm = new(_ServiceTypeReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);
            if(vm.Success)
            {
                return Ok(vm);
            }

            return BadRequest(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]ServiceTypeDeleteRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validatorDelete.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            ServiceTypeDeleteViewModel vm = new(_ServiceTypeReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                return Ok(vm);
            }

            return BadRequest(vm);
        }
    }
}
