using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Role.Request;
using BaseSolution.Application.DataTransferObjects.RoomBookingDetail;
using BaseSolution.Application.DataTransferObjects.RoomBookingDetail.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Infrastructure.ViewModels.RoomBookingDetail;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomBookingDetailsController : ControllerBase
    {
        private readonly IRoomBookingDetailReadOnlyRepository _RoomBookingDetailReadOnlyRepository;
        private readonly IRoomBookingDetailReadWriteRepository _RoomBookingDetailReadWriteRepository;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        private readonly IValidator<RoomBookingDetailCreateRequest> _validator;
        private readonly IValidator<RoomBookingDetailUpdateRequest> _validatorUpdate;
        private readonly IValidator<RoomBookingDetailUpdate2Request> _validatorUpdate2;
        private readonly IValidator<RoomBookingDetailDeleteRequest> _validatorDelete;

        public RoomBookingDetailsController(IRoomBookingDetailReadOnlyRepository RoomBookingDetailReadOnlyRepository, IRoomBookingDetailReadWriteRepository RoomBookingDetailReadWriteRepository,
            IMapper mapper, ILocalizationService localizationService, IValidator<RoomBookingDetailCreateRequest> validator, IValidator<RoomBookingDetailUpdateRequest> validatorUpdate,
             IValidator<RoomBookingDetailUpdate2Request> validatorUpdate2 , IValidator<RoomBookingDetailDeleteRequest> validatorDelete)
        {
            _RoomBookingDetailReadOnlyRepository = RoomBookingDetailReadOnlyRepository;
            _RoomBookingDetailReadWriteRepository = RoomBookingDetailReadWriteRepository;
            _mapper = mapper;
            _localizationService = localizationService;
            _validator = validator;
            _validatorUpdate = validatorUpdate;
            _validatorUpdate2 = validatorUpdate2;
            _validatorDelete = validatorDelete;
        }
        [HttpGet("getRoomBookingDetailByAdmin")]
        public async Task<IActionResult> GetListRoomBookingDetailByAdmin([FromQuery] ViewRoomBookingDetailWithPaginationRequest request, CancellationToken cancellationToken)
        {
            RoomBookingDetailListWithPaginationViewModel vm = new(_RoomBookingDetailReadOnlyRepository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                PaginationResponse<RoomBookingDetailDTO> result = (PaginationResponse<RoomBookingDetailDTO>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpGet("getRoomBookingDetailByOther")]
        public async Task<IActionResult> GetListRoomBookingDetailByOther([FromQuery] ViewRoomBookingDetailWithPaginationRequest request, CancellationToken cancellationToken)
        {
            RoomBookingDetailWithPaginationByOtherViewModel vm = new(_RoomBookingDetailReadOnlyRepository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            if (vm.Success)
            {
                PaginationResponse<RoomBookingDetailDTO> result = (PaginationResponse<RoomBookingDetailDTO>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }


            [HttpGet("getRoomBookingDetailByRoomBookingId")]
        public async Task<IActionResult> GetRoomBookingDetailByRoomBookingId([FromQuery] Guid idRoomBooking, CancellationToken cancellationToken)
        {
            RoomBookingDetailByRoomBookingIdViewModel vm = new(_RoomBookingDetailReadOnlyRepository, _localizationService);
            await vm.HandleAsync(idRoomBooking, cancellationToken);
            if (vm.Success)
            {
                RoomBookingDetailDTO result = (RoomBookingDetailDTO)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomBookingDetailByAdmin(Guid id, CancellationToken cancellationToken)
        {
            RoomBookingDetailViewModel vm = new(_RoomBookingDetailReadOnlyRepository, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("{idRoomBooking}/details")]
        public async Task<IActionResult> GetRoomBookingDetailByIdRoomBooking(Guid idRoomBooking, CancellationToken cancellationToken)
        {
            RoomBookingDetailViewByIdRoomBookingModel vm = new(_RoomBookingDetailReadOnlyRepository, _localizationService);
            await vm.HandleAsync(idRoomBooking, cancellationToken);
            if (vm.Success)
            {
                RoomBookingDetailDTO result = (RoomBookingDetailDTO)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Post(RoomBookingDetailCreateRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validator.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            RoomBookingDetailCreateViewModel vm = new(_RoomBookingDetailReadOnlyRepository, _RoomBookingDetailReadWriteRepository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Put(RoomBookingDetailUpdateRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validatorUpdate.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            RoomBookingDetailUpdateViewModel vm = new(_RoomBookingDetailReadWriteRepository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }
        [HttpPut("updateRoomBookingDetail")]
        public async Task<IActionResult> UpdateRoomBookingDetail(RoomBookingDetailUpdate2Request request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validatorUpdate2.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            RoomBookingDetailUpdate2ViewModel vm = new(_RoomBookingDetailReadWriteRepository, _mapper, _localizationService);
            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(RoomBookingDetailDeleteRequest request, CancellationToken cancellationToken)
        {
            ValidationResult validate = await _validatorDelete.ValidateAsync(request);
            if (!validate.IsValid)
            {
                validate.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }
            RoomBookingDetailDeleteViewModel vm = new(_RoomBookingDetailReadWriteRepository, _localizationService, _mapper);

            await vm.HandleAsync(request, cancellationToken);

            return Ok(vm);
        }
    }
}
