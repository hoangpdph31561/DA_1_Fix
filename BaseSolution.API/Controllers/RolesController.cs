using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Amenity.Request;
using BaseSolution.Application.DataTransferObjects.Role;
using BaseSolution.Application.DataTransferObjects.Role.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Infrastructure.Implements.Repositories.ReadWrite;
using BaseSolution.Infrastructure.ViewModels.Role;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleReadOnlyRepository _roleReadOnlyRepository;
        private readonly IRoleReadWriteRepository _roleReadWriteRepository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        private readonly IValidator<RoleCreateRequest> _validator;
        private readonly IValidator<RoleUpdateRequest> _validatorUpdate;
        private readonly IValidator<RoleDeleteRequest> _validatorDelete;

        public RolesController(IRoleReadOnlyRepository roleReadOnlyRepository, IRoleReadWriteRepository roleReadWriteRepository, IMapper mapper, ILocalizationService localizationService,
            IValidator<RoleCreateRequest> validator, IValidator<RoleUpdateRequest> validatorUpdate,
            IValidator<RoleDeleteRequest> validatorDelete
            )
        {
            _roleReadOnlyRepository = roleReadOnlyRepository;
            _roleReadWriteRepository = roleReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService; 
            _validator = validator;
            _validatorUpdate = validatorUpdate;
            _validatorDelete = validatorDelete;
        }
        [HttpGet]
        public async Task<IActionResult> GetListRoleByAdmin([FromQuery] ViewRoleWithPaginationRequest request, CancellationToken cancellationToken)
        {
            RoleListWithPaginationViewModel vm = new(_roleReadOnlyRepository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if(vm.Success)
            {
                PaginationResponse<RoleDTO> result = (PaginationResponse<RoleDTO>)vm.Data!;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(Guid id, CancellationToken cancellationToken)
        {
            RoleViewModel vm = new(_roleReadOnlyRepository, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            return Ok(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewRole(RoleCreateRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validator.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            RoleCreateViewModel vm = new(_roleReadOnlyRepository, _roleReadWriteRepository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(RoleUpdateRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validatorUpdate.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            RoleUpdateViewModel vm = new(_roleReadWriteRepository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRole(RoleDeleteRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validatorDelete.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            RoleDeleteViewModel vm = new(_roleReadWriteRepository, _mapper, _localizationService);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }
    }
}
