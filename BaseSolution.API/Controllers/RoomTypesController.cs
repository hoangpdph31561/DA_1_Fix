using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Role.Request;
using BaseSolution.Application.DataTransferObjects.RoomType;
using BaseSolution.Application.DataTransferObjects.RoomType.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Infrastructure.ViewModels.RoomType;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypesController : ControllerBase
    {
        private readonly IRoomTypeReadOnlyRepository _RoomTypeReadOnlyRespository;
        private readonly IRoomTypeReadWriteRepository _RoomTypeReadWriteRespository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        private readonly IValidator<RoomTypeCreateRequest> _validator;
        private readonly IValidator<RoomTypeUpdateRequest> _validatorUpdate;
        private readonly IValidator<RoomTypeDeleteRequest> _validatorDelete;

        public RoomTypesController(IRoomTypeReadOnlyRepository RoomTypeReadOnlyRepository, IRoomTypeReadWriteRepository RoomTypeReadWriteRepository, IMapper mapper, ILocalizationService localizationService,
              IValidator<RoomTypeCreateRequest> validator, IValidator<RoomTypeUpdateRequest> validatorUpdate, IValidator<RoomTypeDeleteRequest> validatorDelete)
        {
            _RoomTypeReadOnlyRespository = RoomTypeReadOnlyRepository;
            _RoomTypeReadWriteRespository = RoomTypeReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
            _validator = validator;
            _validatorUpdate = validatorUpdate;
            _validatorDelete = validatorDelete;

        }
        [HttpGet]
        public async Task<IActionResult> GetListRoomTypeByAdmin([FromQuery] ViewRoomTypeWithPaginationRequest request, CancellationToken cancellationToken)
        {
            RoomTypeListWithPaginationViewModel vm = new(_RoomTypeReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                PaginationResponse<RoomTypeDTO> result = (PaginationResponse<RoomTypeDTO>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomTypeById(Guid id, CancellationToken cancellationToken)
        {
            RoomTypeViewModel vm = new(_RoomTypeReadOnlyRespository, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            if (vm.Success)
            {
                RoomTypeDTO result = (RoomTypeDTO)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewRoomType(RoomTypeCreateRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validator.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            RoomTypeCreateViewModel vm = new(_RoomTypeReadOnlyRespository, _RoomTypeReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if(vm.Success)
            {
                return Ok(vm);
            }
            return BadRequest(vm);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoomType(RoomTypeUpdateRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validatorUpdate.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            RoomTypeUpdateViewModel vm = new(_RoomTypeReadWriteRespository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                return Ok(vm);
            }
            return BadRequest(vm);
            
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRoomType([FromQuery]RoomTypeDeleteRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validatorDelete.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            RoomTypeDeleteViewModel vm = new(_RoomTypeReadWriteRespository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            if (vm.Success)
            {
                return Ok(vm);
            }
            return BadRequest(vm);
        }
    }
}
