using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Role.Request;
using BaseSolution.Application.DataTransferObjects.RoomDetail;
using BaseSolution.Application.DataTransferObjects.RoomDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Infrastructure.Implements.Repositories.ReadOnly;
using BaseSolution.Infrastructure.Implements.Repositories.ReadWrite;
using BaseSolution.Infrastructure.ViewModels.Customer;
using BaseSolution.Infrastructure.ViewModels.RoomDetail;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomDetailsController : ControllerBase
    {
        public readonly IRoomDetailReadOnlyRepository _RoomDetailReadOnlyRepository;
        public readonly IRoomDetailReadWriteRepository _RoomDetailReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        private readonly IValidator<RoomDetailCreateRequest> _validator;
        private readonly IValidator<RoomDetailUpdateRequest> _validatorUpdate;
        private readonly IValidator<RoomDetailDeleteRequest> _validatorDelete;

        public RoomDetailsController(IRoomDetailReadOnlyRepository RoomDetailReadOnlyRepository, IRoomDetailReadWriteRepository RoomDetailReadWriteRepository, ILocalizationService localizationService, IMapper mapper,
             IValidator<RoomDetailCreateRequest> validator, IValidator<RoomDetailUpdateRequest> validatorUpdate, IValidator<RoomDetailDeleteRequest> validatorDelete)
        {
            _RoomDetailReadOnlyRepository = RoomDetailReadOnlyRepository;
            _RoomDetailReadWriteRepository = RoomDetailReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
            _validator = validator;
            _validatorUpdate = validatorUpdate;
            _validatorDelete = validatorDelete;

        }
        [HttpGet]
        public async Task<IActionResult> GetListRoomDetailByAdmin([FromQuery] ViewRoomDetailWithPaginationRequest request, CancellationToken cancellationToken)
        {
            RoomDetailListWithPaginationViewModel vm = new(_RoomDetailReadOnlyRepository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);
            if(vm.Success)
            {
                PaginationResponse<RoomDetailDto> result = (PaginationResponse<RoomDetailDto>)vm.Data;
                return Ok(result);
            }

            return BadRequest(vm);
        }

          [HttpGet("getRoomBookingDetailByStatus")]
        public async Task<IActionResult> GetListRoomDetailByStatus([FromQuery] ViewRoomDetailByCheckInCheckOutRequest request, CancellationToken cancellationToken)
        {
            RoomDetailWithPaginationByStatusViewModel vm = new(_RoomDetailReadOnlyRepository, _localizationService);

            await vm.HandleAsync(request, cancellationToken);
            if(vm.Success)
            {
                PaginationResponse<RoomDetailDto> result = (PaginationResponse<RoomDetailDto>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomDetailById(Guid id, CancellationToken cancellationToken)
        {
            RoomDetailViewModel vm = new(_RoomDetailReadOnlyRepository, _localizationService);

            await vm.HandleAsync(id, cancellationToken);
            if(vm.Success)
            {
                RoomDetailDto result = (RoomDetailDto)vm.Data;
                return Ok(result);
            }

            return BadRequest(vm);
        }

        [HttpGet("idRoomType")]
        public async Task<List<RoomDetailDto>> GetRoomDetailByIdRoomType(Guid idRoomType, CancellationToken cancellationToken)
        {
            RoomDetailViewModelByRoomTypeId vm = new(_RoomDetailReadOnlyRepository, _localizationService);

            await vm.HandleAsync(idRoomType, cancellationToken);
            if (vm.Success)
            {
                List<RoomDetailDto> result = (List<RoomDetailDto>)vm.Data;
                return result;
            }

            return new List<RoomDetailDto>();
        }

        [HttpPost]
        public async Task<IActionResult> Post(RoomDetailCreateRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validator.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            RoomDetailCreateViewModel vm = new(_RoomDetailReadOnlyRepository, _RoomDetailReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);
            if(vm.Success)
            {
                return Ok(vm);
            }
            return BadRequest(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Put(RoomDetailUpdateRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validatorUpdate.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            RoomDetailUpdateViewModel vm = new(_RoomDetailReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                return Ok(vm);
            }
            return BadRequest(vm);
        }
        [AllowAnonymous]
        [HttpPut("UpdateStatusRoomDetail")] 
        public async Task<IActionResult> UpdateStatus([FromBody] RoomDetailUpdateStatusRequest request, CancellationToken cancellationToken)
        {
            RoomDetailUpdateStatusViewModel vm = new(_RoomDetailReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                return Ok(vm);
            }
            return BadRequest(vm);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]RoomDetailDeleteRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validatorDelete.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            RoomDetailDeleteViewModel vm = new(_RoomDetailReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            if (vm.Success)
            {
                return Ok(vm);
            }
            return BadRequest(vm);
        }
    }
}
