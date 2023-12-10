using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Role.Request;
using BaseSolution.Application.DataTransferObjects.Roombooking;
using BaseSolution.Application.DataTransferObjects.Roombooking.Request;
using BaseSolution.Application.DataTransferObjects.RoomBooking.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Pagination;
using BaseSolution.Infrastructure.ViewModels.Roombooking;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace BaseSolution.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoombookingsController : ControllerBase
{
    private readonly IRoombookingReadOnlyRepository _roombookingrReadOnlyRespository;
    private readonly IRoombookingReadWriteRepository _roombookingReadWriteRespository;
    private readonly IMapper _mapper;
    private readonly ILocalizationService _localizationService;
    private readonly IValidator<RoombookingCreateRequest> _validator;
    private readonly IValidator<RoombookingUpdateRequest> _validatorUpdate;
    private readonly IValidator<RoombookingDeleteRequest> _validatorDelete;

    public RoombookingsController(IRoombookingReadOnlyRepository roombookingReadOnlyRepository, IRoombookingReadWriteRepository roombookingReadWriteRepository, IMapper mapper, ILocalizationService localizationService,
          IValidator<RoombookingCreateRequest> validator, IValidator<RoombookingUpdateRequest> validatorUpdate, IValidator<RoombookingDeleteRequest> validatorDelete)

    {
        _roombookingrReadOnlyRespository = roombookingReadOnlyRepository;
        _roombookingReadWriteRespository = roombookingReadWriteRepository;
        _mapper = mapper;
        _localizationService = localizationService;
        _validator = validator;
        _validatorUpdate = validatorUpdate;
        _validatorDelete = validatorDelete;

    }
    [HttpGet("getRoomBookingByDetail")]
    public async Task<IActionResult> GetListRoomBookingDetailByAdmin([FromQuery] ViewRoombookingWithPaginationRequest request, CancellationToken cancellationToken)
    {
        RoombookingListWithPaginationViewModel vm = new(_roombookingrReadOnlyRespository, _localizationService);
        await vm.HandleAsync(request, cancellationToken);
        if (vm.Success)
        {
            PaginationResponse<RoombookingDTO> result = (PaginationResponse<RoombookingDTO>)vm.Data;
            return Ok(result);
        }
        return BadRequest(vm);

    }
      [HttpGet("getRoomBookingByOther")]
    public async Task<IActionResult> GetListRoomBookingDetailByOther([FromQuery] ViewRoombookingWithPaginationRequest request, CancellationToken cancellationToken)
    {
        RoomBookingListWithPaginationByOtherViewModel vm = new(_roombookingrReadOnlyRespository, _localizationService);
        await vm.HandleAsync(request, cancellationToken);

        if(vm.Success)

        {
            PaginationResponse<RoombookingDTO> result = (PaginationResponse<RoombookingDTO>)vm.Data;
            return Ok(result);
        }
        return BadRequest(vm);
    }
    [HttpGet("getRoomBookingByAwait")]
    public async Task<IActionResult> GetListRoomBookingDetailByAwait([FromQuery] ViewRoombookingWithPaginationRequest request, CancellationToken cancellationToken)
    {
        RoomBookingListWithPaginationByAwaitViewModel vm = new(_roombookingrReadOnlyRespository, _localizationService);
        await vm.HandleAsync(request, cancellationToken);

        if (vm.Success)

        {
            PaginationResponse<RoombookingDTO> result = (PaginationResponse<RoombookingDTO>)vm.Data;
            return Ok(result);
        }
        return BadRequest(vm);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoomBookingDetailByAdmin(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            RoombookingViewModel vm = new(_roombookingrReadOnlyRespository, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            if(vm.Success)
            {
                RoombookingDTO result = (RoombookingDTO)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        catch (Exception)
        {
            throw;
        }
       
    }
    [HttpGet("{idCustomer}/details")]
    public async Task<IActionResult> GetRoomBookingDetailByIdCustomer(Guid idCustomer, CancellationToken cancellationToken)
    {
        try
        {
            RoomBookingByCustomerIdViewModel vm = new(_roombookingrReadOnlyRespository, _localizationService);
            await vm.HandleAsync(idCustomer, cancellationToken);
            if (vm.Success)
            {
                List<RoombookingDTO> result = (List<RoombookingDTO>)vm.Data;
                return Ok(result);
            }
            return BadRequest(vm);
        }
        catch (Exception)
        {
            throw;
        }
    }
    [HttpPost]
    public async Task<IActionResult> Post(RoombookingCreateRequest request, CancellationToken cancellationToken)
    {
        ValidationResult validate = await _validator.ValidateAsync(request);
        if (!validate.IsValid)
        {
            validate.AddToModelState(this.ModelState);
            return BadRequest(ModelState);
        }
        RoombookingCreateViewModel vm = new(_roombookingrReadOnlyRespository, _roombookingReadWriteRespository, _mapper, _localizationService);
        await vm.HandleAsync(request, cancellationToken);

        return Ok(vm.Data);
    }
    [HttpPost("postByCustomer")]
    public async Task<IActionResult> PostByCustomer(RoombookingCreateRequest request, CancellationToken cancellationToken)
    {
        ValidationResult validate = await _validator.ValidateAsync(request);
        if (!validate.IsValid)
        {
            validate.AddToModelState(this.ModelState);
            return BadRequest(ModelState);
        }
        RoomBookingCreateByCustomerViewModel vm = new(_roombookingrReadOnlyRespository, _roombookingReadWriteRespository, _mapper, _localizationService);
        await vm.HandleAsync(request, cancellationToken);

        return Ok(vm.Data);
    }

    [HttpPut]
    public async Task<IActionResult> Put(RoombookingUpdateRequest request, CancellationToken cancellationToken)
    {
        ValidationResult validate = await _validatorUpdate.ValidateAsync(request);
        if (!validate.IsValid)
        {
            validate.AddToModelState(this.ModelState);
            return BadRequest(ModelState);
        }
        RoombookingUpdateViewModel vm = new(_roombookingReadWriteRespository, _mapper, _localizationService);
        await vm.HandleAsync(request, cancellationToken);

        return Ok(vm);
    }
    [HttpPut("updateStatus")]
    public async Task<IActionResult> UpdateStatusRoomBooking(RoomBookingUpdateStatusRequest request, CancellationToken cancellationToken)
    {
        RoomBookingUpdateStatusViewModel vm = new(_roombookingReadWriteRespository, _mapper, _localizationService);
        await vm.HandleAsync(request, cancellationToken);

        return Ok(vm.Data);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(RoombookingDeleteRequest request, CancellationToken cancellationToken)
    {
        ValidationResult validate = await _validatorDelete.ValidateAsync(request);
        if (!validate.IsValid)
        {
            validate.AddToModelState(this.ModelState);
            return BadRequest(ModelState);
        }
        RoombookingDeleteViewModel vm = new(_roombookingReadWriteRespository, _localizationService, _mapper);

        await vm.HandleAsync(request, cancellationToken);

        return Ok(vm);
    }
}
